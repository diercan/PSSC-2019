using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ml_stats_webapp.Options;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace ml_stats_webapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            var settings = config.Build();
                            var mode = (KeyVaultUsage) Enum.Parse(typeof(KeyVaultUsage), (settings["Secrets:Mode"]));
                            if (mode != KeyVaultUsage.UseLocalSecretStorage)
                            {
                                KeyVaultOptions kvc = settings.GetSection("Secrets").Get<KeyVaultOptions>();
                                if (mode == KeyVaultUsage.UseClientSecret)
                                {
                                    config.AddAzureKeyVault(kvc.KeyVaultUri, kvc.ClientId, kvc.ClientSecret);
                                }
                                else
                                {
                                    var tokenProvider = new AzureServiceTokenProvider();
                                    var kvcClient = new KeyVaultClient((authority, resource, scope) => tokenProvider.KeyVaultTokenCallback(authority, resource, scope));
                                    config.AddAzureKeyVault(kvc.KeyVaultUri, kvcClient,
                                        new DefaultKeyVaultSecretManager());
                                }
                            }
                        })
                        .UseStartup<Startup>();
                });
    }
}
