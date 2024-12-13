using AutoMapper;
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
            //Get Region Domain Model from the db
            var regionDomain = await regionRepository.GetByIdAsync(id); //using repository pattern 

            if (regionDomain == null)
            {
                return NotFound(); //404
            }

            // Map Domain Model to DTO 
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            //return DTO back to the client
            return Ok(regionDto); 
        }

        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        //[FRomBody] in parameter because in the post method we receive the body from the client
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto) 
        {
            // Map DTO to Domain Model

            var regionDomainModel = new Region()
            {
                //here we have 3 properties to map
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl= addRegionRequestDto.RegionImageUrl
            };

            //following repository pattern
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

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

        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto) 
        {
            //Map DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel); //following repository pattern

            if (regionDomainModel == null)
            {
                return NotFound();
            }

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
