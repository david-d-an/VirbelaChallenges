using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Exercise1.Api.Config
{
    public class ApiConfig
    {
        private static List<string> GetConfigSection(IConfiguration cfg, string sectionName) {
            List<string> section = new List<string>();
            foreach (var i in cfg.GetSection(sectionName).GetChildren()) {
                section.Add(i.Value);
            }
            return section;
        }

        public static SecuritySettings GetSecuritySettings(
            IConfiguration cfg) 
            // ILogger<Startup> logger)
        {
            // var redirectUris = GetConfigSection(cfg, "RedirectUris");
            // var postLogoutRedirectUris = GetConfigSection(cfg, "PostLogoutRedirectUris");
            // var allowedCorsOrigins = GetConfigSection(cfg, "AllowedCorsOrigins");
            var security = new SecuritySettings {
                ApiName = cfg["ApiName"],
                StsAuthority = cfg["StsAuthority"],
                AllowedCorsOrigins = GetConfigSection(cfg, "AllowedCorsOrigins")
            };

            // logger.LogInformation(string.Format("{0}: {1}", "ApiName", security.ApiName));
            // logger.LogInformation(string.Format("{0}: {1}", "ClientName", security.StsAuthority));
            // foreach(var s in security.AllowedCorsOrigins) {
            //     logger.LogInformation(string.Format("{0}: {1}", "AllowedCorsOrigins", s));
            // }

            return security;
        }
    }
}