using CoreCodedChatbot.Client.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CoreCodedChatbot.Client.Services
{
    public class GetLocalHubUrlService : IGetLocalHubUrlService
    {
        public string Get(HttpRequest request, string hubPath)
        {
            var securityLevel = request.IsHttps ? "https://" : "http://";

            var hubUrl = $"{securityLevel}{request.Host}{hubPath}";

            return hubUrl;
        }
    }
}