using Microsoft.AspNetCore.Mvc;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        public IActionResult Index()
        {
            //Get all regions from Web API (and then pass this information to the View to display it)
            return View();
        }
    }
}
