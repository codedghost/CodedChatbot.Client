using System.Threading.Tasks;
using CoreCodedChatbot.Client.Models;
using Microsoft.AspNetCore.SignalR;

namespace CoreCodedChatbot.Client.Hubs
{
    public class BackgroundSongHub : Hub
    {
        public async Task SendSongInfo(PlaybackModel model)
        {
            await Clients.All.SendAsync("UpdateSongInfo", model);
        }
    }
}