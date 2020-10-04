using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreCodedChatbot.Client.Interfaces;
using CoreCodedChatbot.Client.Models;
using CoreCodedChatbot.Config;
using Newtonsoft.Json;

namespace CoreCodedChatbot.Client.Services
{
    public class YTMusicPlayerService : IYTMusicPlayerService
    {
        private readonly IConfigService _configService;

        private readonly HttpClient _client;

        public YTMusicPlayerService(IConfigService configService)
        {
            _configService = configService;

            var ytBaseAddress = _configService.Get<string>("YTPlayerBaseAddress");

            _client = new HttpClient
            {
                BaseAddress = new Uri(ytBaseAddress)
            };
        }

        public async Task<PlaybackModel> Query()
        {
            var request = await _client.GetAsync("query");

            if (!request.IsSuccessStatusCode) return null;

            var contentString = await request.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<PlaybackModel>(contentString);

            return data;

        }
    }
}