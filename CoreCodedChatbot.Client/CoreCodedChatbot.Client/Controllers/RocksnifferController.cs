using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreCodedChatbot.Client.Models.Rocksniffer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoreCodedChatbot.Client.Controllers
{
    public class RocksnifferController : Controller
    {
        private HttpClient _httpClient;
        private const string _snifferIp = "http://127.0.0.1";
        private const string _snifferPort = "9938";

        private string _snifferUrl = $"{_snifferIp}:{_snifferPort}";

        public RocksnifferController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_snifferUrl)
            };
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var (snifferOutput, _) = await GetSnifferOutput().ConfigureAwait(false);

            return Json(
                new
                {
                    FormattedOutput = snifferOutput
                });
        }

        [HttpGet]
        public async Task<IActionResult> Raw()
        {
            var (_, json) = await GetSnifferOutput().ConfigureAwait(false);

            return Content(json);
        }

        private async Task<(SnifferOutput output, string rawJson)> GetSnifferOutput()
        {
            var snifferResponse = await _httpClient.GetAsync("");

            if (snifferResponse.IsSuccessStatusCode)
            {
                var jsonString = await snifferResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                var songDetails = JsonConvert.DeserializeObject<SnifferOutput>(jsonString);
                Console.WriteLine(JsonConvert.SerializeObject(songDetails));

                return (songDetails, jsonString);
            }

            Console.WriteLine("Could not query Rocksniffer");

            return (null, null);
        }
    }
}