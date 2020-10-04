using System.Threading.Tasks;
using CoreCodedChatbot.Client.Models;

namespace CoreCodedChatbot.Client.Interfaces
{
    public interface IYTMusicPlayerService
    {
        Task<PlaybackModel> Query();
    }
}