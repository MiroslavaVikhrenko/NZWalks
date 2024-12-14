using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    //https://localhost:portnumber/api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        //injections
        private readonly IMapper mapper;
        public WalksController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        //CREATE Walk
        //POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto); //using AutoMapper
        }
    }
}
