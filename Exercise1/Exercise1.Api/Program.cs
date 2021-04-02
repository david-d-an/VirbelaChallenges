using System;
using System.IO;
using Exercise1.DataAccess.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace Exercise1.Api
{
    public class Program
    {
        private static bool IsDevelopment =>
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
            .AddJsonFile($"appsettings.Development.json", true)
            .AddEnvironmentVariables()
            .Build();

        public static Logger Logger { get; } = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        public static int Main(string[] args)
        {
            Log.Logger = Logger;

            try {
                Log.Information("Starting up");
                IHost host = CreateHostBuilder(args).Build();
                host.Run();
                return 0;
            } catch (Exception ex) {
                Log.Fatal(ex, "Application start-up failed");
                return 1;
            } finally {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(SetupConfiguration)
                .ConfigureLogging(logging => {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .UseSerilog((hostingContext, loggerConfig) =>
                    loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
                )
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SetupConfiguration(HostBuilderContext ctx, IConfigurationBuilder builder)
        {
            builder
                .AddJsonFile("Config/config.json", false, true)
                .AddEnvironmentVariables();
        }

        private static void SeedEmployeeDatabase(IHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope()) {
                var seeder = scope.ServiceProvider.GetService<VirbelaListingDataSeeder>();
                seeder.Seed();
            }
        }
    }
}
