using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
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
    [Authorize] //Microsoft.AspNetCore.Authorization

    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        //inject db context, repository, automapper
        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET: https://localhost:portnumber/api/regions (RESTful URL)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from the db = Domain Models
            var regionsDomain = await regionRepository.GetAllAsync(); //using repository pattern 

            //Map Domain Models to DTOs
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain); //using AutoMapper

            //Return DTOs back to the client
            return Ok(regionsDto);
        }

        
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")] //'id' in attribute MUST match the name of input parameter passed to the method for proper mapping
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Region Domain Model from the db following repository pattern
            var regionDomain = await regionRepository.GetByIdAsync(id); 

            if (regionDomain == null)
            {
                return NotFound(); //404
            }

            // Map Domain Model to DTO 
            var regionDto = mapper.Map<RegionDto>(regionDomain); //using AutoMapper

            //return DTO back to the client
            return Ok(regionDto); 
        }

        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        //[FromBody] in parameter because in the post method we receive the body from the client
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto) 
        {
            // Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto); //using AutoMapper

            //following repository pattern
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //Map Domain Model back to DTO 
            var regionDto = mapper.Map<RegionDto>(regionDomainModel); //using AutoMapper

            //for post method we need to return 201
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto) 
        {
            //Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto); //using AutoMapper

            //following repository pattern
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //convert Domain Model to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel); //using AutoMapper

            return Ok(regionDto);

        }

        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //follow repository pattern
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //optionally we can return the deleted region back - in this case we would need to map the domain model to dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel); //using AutoMapper

            return Ok(regionDto); //but we can alternative respond with empty Ok();
        }
    }
}
