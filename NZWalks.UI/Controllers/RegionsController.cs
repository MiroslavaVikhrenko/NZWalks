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
        public IActionResult Index()
        {
            //Get all regions from Web API (and then pass this information to the View to display it)
            return View();
        }
    }
}
