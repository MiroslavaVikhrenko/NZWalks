using Microsoft.AspNetCore.Mvc;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
