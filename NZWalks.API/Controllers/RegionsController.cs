using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

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
            //Get data from the db = Domain Models
            var regionsDomain = dbContext.Regions.ToList(); 

            //Map Domain Models to DTOs - convert Domain Model to DTO which is also a list
            var regionsDto = new List<RegionDto>();

            //loop over regions we have and convert all these regions to RegionDto
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    //map individual properties
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            //Return DTOs back to the client
            return Ok(regionsDto);
        }

        
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")] //'id' in attribute MUST match the name of input parameter passed to the method for proper mapping
        public IActionResult GetRegionBYId([FromRoute] Guid id)
        {
            //Get Region Domain Model from the db

            //First option using dbContext.Regions.Find(id)
            //the Find() method only takes the primary key (=id) => you cannot use other properties like Name/Code/etc
            /*var region = dbContext.Regions.Find(id);*/ //find will take the primary key (=id)

            //Second option using dbContext.Regions.FirstOrDefault(x => x.Id == id)
            //you can do this for id or using other properties (Name, Code, etc) BUT only if you are passing those in the route
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id); 

            if (regionDomain == null)
            {
                return NotFound(); //404
            }

            // Map/Convert Region Domain Model to Region DTO
            var regionDto = new RegionDto()
            {
                //map individual properties
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            //return DTO back to the client
            return Ok(regionDto); 
        }

        //Action method to create a new region
        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        public IActionResult Create()
        {
            //we want 3 pieces of info from the client: Name, Code and Image URL. We do not want an ID because it will be created by the app internally
            //so now we want to craete a DTO for this purpose with just needed 3 properties to get info from the client and then map it to the domain model
        }
    }
}
