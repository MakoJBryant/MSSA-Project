using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Logging;
using Project;

namespace Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        // Call the startup file, using azure key vault secrets.
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, config) =>
            {
                // https://docs.microsoft.com/en-us/aspnet/core/security/key-vault-configuration?view=aspnetcore-2.2
                if (context.HostingEnvironment.IsProduction())
                {
                    var builtConfig = config.Build();
                    var keyVaultEndpoint = "https://theazuretube-kv-0.vault.azure.net/";
                    var azureServiceTokenProvider = new AzureServiceTokenProvider();
                    var keyVaultClient = new KeyVaultClient(
                        new KeyVaultClient.AuthenticationCallback(
                            azureServiceTokenProvider.KeyVaultTokenCallback));
                    
                    config.AddAzureKeyVault(
                        keyVaultEndpoint,
                        keyVaultClient, 
                        new DefaultKeyVaultSecretManager()
                        );
                }
            })
            .UseStartup<Startup>();
    }
}
