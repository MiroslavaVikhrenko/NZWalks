using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    //https://localhost:portnumber/api/regions
    [Route("api/[controller]")] //defining the route whenever a user enters this route along with the app URL,
                                //it will be pointed to the RegionsController
    [ApiController] //<= This attribute will tell the app that this controller is for API use

    public class RegionsController : ControllerBase
    {
        //we already injected db context in Program.cs so we can now use db context here as well through constructor injection
        private readonly NZWalksDbContext dbContext;
        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Action method to return all regions
        // GET: https://localhost:portnumber/api/regions (RESTful URL)
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList(); //getting data from the db
            return Ok(regions);
        }
    }
}
