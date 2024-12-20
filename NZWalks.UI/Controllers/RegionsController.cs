using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.DTO;

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
    }
}
