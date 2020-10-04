using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR.Client;

namespace CoreCodedChatbot.Client.Interfaces
{
    public interface IGetLocalHubUrlService
    {
        string Get(HttpRequest request, string hubPath);
    }
}