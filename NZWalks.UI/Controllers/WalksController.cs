using Microsoft.AspNetCore.Mvc;

namespace NZWalks.UI.Controllers
{
    public class WalksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
