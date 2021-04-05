using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Exercise1.Common.Security;

using Exercise1.Data.Repos;
using Exercise1.DataAccess.Context;
using Exercise1.DataAccess.Repos.VirbelaListing;
using Exercise1.Api.Config;
using Exercise1.Api.Authentication;
using Exercise1.Api.Authentication.Provider;
// using Microsoft.OpenApi.Models;

namespace Exercise1.Api
{
    public class Startup
    {
        private SecuritySettings securitySettings;
        private readonly string Exercise1WebOrigins = "Excercise1.Web";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            this.securitySettings = ApiConfig.GetSecuritySettings(Configuration);

            // services.AddSwaggerGen(c => c.SwaggerDoc(
            //     name: "v1",
            //     new OpenApiInfo() {
            //         Title = "My API",
            //         Version = "v1"
            //     })
            // );

            // This is where CORS is set up, in case Web App connection is needed.
            services.AddCors(options => {
                options.AddPolicy(name: Exercise1WebOrigins, builder => {
                    builder
                    .AllowAnyHeader()
                    .WithOrigins(
                        securitySettings.AllowedCorsOrigins.ToArray()
                    )
                    .AllowAnyMethod();
                });
            });
            services.AddControllers();

            services.AddResponseCaching();

            // I set up two databaes for DEV and Staging, see appsetting.{env_name}.json
            // to get environment specific connection strings.
            var encConnStrVirbelaListing = Configuration.GetConnectionString("VirbelaListing(Azure)");
            var connStrVirbelaListing = AesCryptoUtil.Decrypt(encConnStrVirbelaListing);
            services.AddDbContext<VirbelaListingContext>(builder =>                   
                builder.UseSqlServer(connStrVirbelaListing)
            );
            EnsureDatabaseExists<VirbelaListingContext>(connStrVirbelaListing);

            // UnitOfWork is injected and all repoistory injections are removed.
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }

        private static void EnsureDatabaseExists<T>(string connectionString) 
        where T : DbContext, new()
        {
            /* Generic function that can work with severl different */ 
            /* Database type. Since I use SQL Database only, the    */
            /* rest is commented out.                               */
            var builder = new DbContextOptionsBuilder<T>();
            if (typeof(T) == typeof(VirbelaListingContext)) {
                builder.UseSqlServer(connectionString);
            }
            // else if (typeof(T) == typeof(MySqlContext)) {
            //     builder.UseMySQL(connectionString);
            // }
            // else if (typeof(T) == typeof(SQLiteContext)) {
            //     builder.UseSqlite(connectionString);
            // }
            // else if (typeof(T) == typeof(SqlServerContext)) {
            //     builder.UseSqlServer(connectionString);
            // }

            using var context = (T)Activator.CreateInstance(typeof(T), 
                new object[] { builder.Options });
            context.Database.EnsureCreated();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            // General security settings are printed in the log file during app start up.
            // This output will be very helpful during deployment in case environmental variable
            // troubles you.
            foreach (var s in securitySettings.AllowedCorsOrigins) {
                logger.LogInformation(string.Format("{0}: {1}", "AllowedCorsOrigins", s));
            }
            logger.LogInformation(string.Format("{0}: {1}", "StsAuthority", securitySettings.StsAuthority));
            logger.LogInformation(string.Format("{0}: {1}", "ApiName", securitySettings.ApiName));

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/error");
            }

            // app.UseSwagger();
            // app.UseSwaggerUI(c => c.SwaggerEndpoint(
            //     url: "/swagger/v1/swagger.json",
            //     name: "My API V1"
            // ));

            app.UseRouting();
            
            app.UseCors(Exercise1WebOrigins);

            // app.UseHttpsRedirection();

            // custom jwt auth middleware replaces builtin Auth
            app.UseMiddleware<JwtMiddleware>();
            // app.UseAuthentication();
            // app.UseAuthorization();

            // Enable response cache
            app.UseResponseCaching();
            
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
