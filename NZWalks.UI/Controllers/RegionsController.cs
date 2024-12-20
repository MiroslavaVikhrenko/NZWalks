using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Index()
        {
            try
            {
                //Get all regions from Web API (and then pass this information to the View to display it)
                var client = httpClientFactory.CreateClient(); //creates a new http client which can be used to consume the Web API

                //use the get method from API project's controller
                var httpResponseMessage = await client.GetAsync("https://localhost:7008/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                //extract the body of the response
                var stringResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();

                ViewBag.Response = stringResponseBody;
            }
            catch (Exception)
            {
                //Log the exception
                throw;
            }

            return View();
        }
    }
}
