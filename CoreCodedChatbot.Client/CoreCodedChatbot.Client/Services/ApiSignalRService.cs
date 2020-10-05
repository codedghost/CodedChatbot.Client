using System;
using System.Threading;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.ClientTrigger;
using CoreCodedChatbot.ApiContract.SignalRHubModels.API;
using CoreCodedChatbot.Client.Interfaces;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Azure.KeyVault.Models;

namespace CoreCodedChatbot.Client.Services
{
    public class ApiSignalRService : IApiSignalRService
    {
        private readonly IYTMusicPlayerService _ytMusicPlayerService;
        private readonly IClientTriggerClient _clientTriggerClient;
        private HubConnection _backgroundSongHubConnection;

        private Timer _reconnectTask;

        public ApiSignalRService(
            IConfigService configService,
            ISecretService secretService,
            IYTMusicPlayerService ytMusicPlayerService,
            IClientTriggerClient clientTriggerClient
        )
        {
            _ytMusicPlayerService = ytMusicPlayerService;
            _clientTriggerClient = clientTriggerClient;
            var baseUrl = configService.Get<string>("ApiBaseAddress");

            _backgroundSongHubConnection = new HubConnectionBuilder()
                .WithUrl($"{baseUrl}{APIHubConstants.BackgroundSongHubPath}", options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult(secretService.GetSecret<string>("JwtTokenString"));
                    })
                .Build();

            _backgroundSongHubConnection.Closed += async (error) =>
            {
                _reconnectTask = new Timer(async e =>
                {
                    await _backgroundSongHubConnection.StartAsync();

                    if (_backgroundSongHubConnection.State == HubConnectionState.Connected)
                    {
                        _reconnectTask.Dispose();
                        _reconnectTask = null;
                    }
                }, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5));
            };

            _backgroundSongHubConnection.On<string>("BackgroundSongCheck", (username) => { BackgroundSongCheck(username); });
            _backgroundSongHubConnection.On("Heartbeat", () => { });

            _backgroundSongHubConnection.StartAsync().Wait();
        }

        public async void BackgroundSongCheck(string username)
        {
            var playbackState = await _ytMusicPlayerService.Query();

            if (playbackState?.TrackModel != null)
            {
                await _clientTriggerClient.SendBackgroundSongResult(new SendBackgroundSongResultRequest
                {
                    Username = username,
                    Title = playbackState.TrackModel.Title,
                    Artist = playbackState.TrackModel.Author,
                    Url = playbackState.TrackModel.WatchUrl
                });
            }
        }
    }
}