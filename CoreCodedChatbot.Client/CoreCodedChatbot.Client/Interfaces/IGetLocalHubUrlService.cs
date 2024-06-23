using Microsoft.AspNetCore.Http;

namespace CoreCodedChatbot.Client.Interfaces
{
    public interface IGetLocalHubUrlService
    {
        string Get(HttpRequest request, string hubPath);
    }
}