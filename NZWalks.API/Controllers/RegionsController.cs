using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    //https://localhost:portnumber/api/regions
    [Route("api/[controller]")] //defining the route whenever a user enters this route along with the app URL,
                                //it will be pointed to the RegionsController
    [ApiController] //<= This attribute will tell the app that this controller is for API use

    public class RegionsController : ControllerBase
    {
        //create action methods

        //Method to return all regions
        // GET: https://localhost:portnumber/api/regions (RESTful URL)
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland Region",
                    Code = "AKL",
                    RegionImageUrl = "https://www.pexels.com/photo/silhouetted-couple-exploring-piha-archway-29249172/"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Wellington Region",
                    Code = "WLG",
                    RegionImageUrl = "https://www.pexels.com/photo/brown-hanging-bridge-surrounded-by-trees-403781/"
                }
            };

            return Ok(regions);
        }
    }
}
