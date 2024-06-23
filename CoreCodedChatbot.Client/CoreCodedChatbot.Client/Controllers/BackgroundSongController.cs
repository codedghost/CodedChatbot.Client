using System;
using System.Diagnostics;
using System.Threading;
using CoreCodedChatbot.Client.Hubs;
using CoreCodedChatbot.Client.Interfaces;
using CoreCodedChatbot.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

namespace CoreCodedChatbot.Client.Controllers
{
    public class BackgroundSongController : Controller
    {
        private readonly IGetLocalHubUrlService _getLocalHubUrlService;
        private readonly IYTMusicPlayerService _ytMusicPlayerService;
        private Timer _checkCurrentSongTimer;
        private HubConnection _signalRConnection;
        public BackgroundSongController(
            IGetLocalHubUrlService getLocalHubUrlService,
            IYTMusicPlayerService ytMusicPlayerService)
        {
            _getLocalHubUrlService = getLocalHubUrlService;
            _ytMusicPlayerService = ytMusicPlayerService;
        }

        public IActionResult Index()
        {
            var hubUrl = _getLocalHubUrlService.Get(Request, HubConstants.BackgroundSongPath);

            Console.Out.WriteLine(hubUrl);

            if (_signalRConnection != null)
            {
                _signalRConnection = new HubConnectionBuilder()
                    .WithUrl(hubUrl)
                    .Build();

                _signalRConnection.StartAsync().Wait();
            }

            _checkCurrentSongTimer = new Timer(async timer =>
            {
                try
                {
                    var playbackModel = await _ytMusicPlayerService.Query();

                    if (playbackModel != null)
                    {
                        await _signalRConnection.InvokeAsync("SendSongInfo", playbackModel);
                    }
                }
                catch (Exception e)
                {

                }
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
