using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    //https://localhost:portnumber/api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        //CREATE Walk
        //POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create()
        {

        }
    }
}
