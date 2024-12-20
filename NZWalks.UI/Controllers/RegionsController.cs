using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        // Inject HttpClientFactory
        private readonly IHttpClientFactory httpClientFactory;      

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();

            try
            {
                //Get all regions from Web API (and then pass this information to the View to display it)
                var client = httpClientFactory.CreateClient(); //creates a new http client which can be used to consume the Web API

                //use the get method from API project's controller
                var httpResponseMessage = await client.GetAsync("https://localhost:7008/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                //add the response to the list
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

                
            }
            catch (Exception)
            {
                //Log the exception
                throw;
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7008/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }
    }
}
