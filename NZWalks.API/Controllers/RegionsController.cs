using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

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
        private readonly IRegionRepository regionRepository;

        //pass IRegionRepository to follow the repository pattern
        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        //Action method to return all regions
        // GET: https://localhost:portnumber/api/regions (RESTful URL)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from the db = Domain Models
            var regionsDomain = await regionRepository.GetAllAsync();

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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Region Domain Model from the db

            //First option using dbContext.Regions.Find(id)
            //the Find() method only takes the primary key (=id) => you cannot use other properties like Name/Code/etc
            /*var region = dbContext.Regions.Find(id);*/ //find will take the primary key (=id)

            //Second option using dbContext.Regions.FirstOrDefault(x => x.Id == id)
            //you can do this for id or using other properties (Name, Code, etc) BUT only if you are passing those in the route
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id); 

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
        //[FRomBody] in parameter because in the post method we receive the body from the client
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto) 
        {
            //we want 3 pieces of info from the client: Name, Code and Image URL. We do not want an ID because it will be created by the app internally
            //so now we want to create a DTO (AddRegionRequestDto) for this purpose with just needed 3 properties to get info from the client and then map it to the domain model

            // Map/Convert DTO to Domain Model
            //Let's create a domain model

            var regionDomainModel = new Region()
            {
                //here we have 3 properties to map
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl= addRegionRequestDto.RegionImageUrl
            };

            //Use Domain Model to create Region, use dbContext to add new region to the db
            await dbContext.Regions.AddAsync(regionDomainModel); //if we execute this line, a new region is NOT added to the db
            //save the new region to the db
            await dbContext.SaveChangesAsync(); //at this line new region is saved to the db and the changes will be reflected in the SQL Server

            //Map Domain Model back to DTO (=we cannot send Domain Model to the client< need to convert back to DTO first)
            var regionDto = new RegionDto
            {
                //now we have to map 4 properties because Id was created by EF
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            //for post method we need to return 201
            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
        }

        //Action method to update a region
        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto) 
        {
            //use id to fist check if the region exists - as we check by id we can use either Find() or FirstOrDefault()
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Update Domain Model with vaues from received DTO 
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            //save changes to db
            await dbContext.SaveChangesAsync();

            //convert Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }

        //Action method to delete a region
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //first check if this region exists
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //if such region was found, we will delete it
            dbContext.Regions.Remove(regionDomainModel); //Remove() method does not have async version
            await dbContext.SaveChangesAsync(); //to actually delete from db

            //optionally we can return the deleted region back - in this case we would need to map the domain model to dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto); //but we can alternative respond with empty Ok();
        }
    }
}
