using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CoreCodedChatbot.Client.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace CoreCodedChatbot.Client.Controllers
{
    public class BackgroundSongController : Controller
    {
        private Timer _checkCurrentSongTimer;
        private HubConnection _signalRConnection;
        private HttpClient _client;

        public BackgroundSongController()
        {
        }

        public IActionResult Index()
        {
            var hubUrl = $"https://{Request.Host}/CurrentSong";
            Console.Out.WriteLine(hubUrl);

            _signalRConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();

            _signalRConnection.StartAsync().Wait();

            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:9863")
            };

            _checkCurrentSongTimer = new Timer(async timer =>
            {
                try
                {
                    var request = await _client.GetAsync("query");

                    if (request.IsSuccessStatusCode)
                    {
                        var contentString = await request.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<PlaybackModel>(contentString);

                        await _signalRConnection.InvokeAsync("SendSongInfo", data);
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
