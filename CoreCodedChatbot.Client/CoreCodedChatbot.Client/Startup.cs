using System;
using CoreCodedChatbot.ApiClient;
using CoreCodedChatbot.Client.Interfaces;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Logging;
using CoreCodedChatbot.Secrets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreCodedChatbot.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configService = new ConfigService();
            var secretService = new AzureKeyVaultService(configService.Get<string>("KeyVaultAppId"),
                configService.Get<string>("KeyVaultCertThumbprint"),
                configService.Get<string>("KeyVaultBaseUrl"));
            secretService.Initialize();

            services.AddOptions();
            services.AddMemoryCache();

            services
                .AddChatbotConfigService()
                .AddChatbotSecretServiceCollection(
                    configService.Get<string>("KeyVaultAppId"),
                    configService.Get<string>("KeyVaultCertThumbprint"),
                    configService.Get<string>("KeyVaultBaseUrl")
                )
                .AddChatbotNLog(secretService)
                .AddApiClientServices()
                .AddGuessingGameServices();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
                
            provider.GetService<IGuessingGameService>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
