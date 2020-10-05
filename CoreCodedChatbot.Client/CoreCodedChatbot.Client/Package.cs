using CoreCodedChatbot.Client.Interfaces;
using CoreCodedChatbot.Client.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoreCodedChatbot.Client
{
    public static class Package
    {
        public static IServiceCollection AddClientServices(this IServiceCollection services)
        {
            services.AddSingleton<IApiSignalRService, ApiSignalRService>();

            services.AddSingleton<IGuessingGameService, GuessingGameService>();

            services.AddSingleton<IGetLocalHubUrlService, GetLocalHubUrlService>();

            services.AddSingleton<IYTMusicPlayerService, YTMusicPlayerService>();

            return services;
        }
    }
}
