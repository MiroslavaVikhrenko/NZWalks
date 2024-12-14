using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile //comes from  AutoMapper
    {
        public AutoMapperProfiles()
        {
            //CreateMap() method needs 2 types - type of source and type of destination
            //if matches - it will do it for us
            //if doesn't match - we need to explicitly define the mapping between those properties in these two models
            //ReverseMap() automatically define reverse map between source and destination

            //Mapping between Region Domain Model and Region DTO (properties' names are the same)
            CreateMap<Region, RegionDto>().ReverseMap();

            //Mapping between Region Domain Model and AddRegionRequest DTO
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();

            //Mapping between Region Domain Model and UpdateRegionRequest DTO
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            //Mapping between Walk Domain Model and AddWalkRequest DTO
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();

            //Mapping between Walk Domain Model and Walk DTO
            CreateMap<Walk, WalkDto>().ReverseMap();

            //Mapping between Difficulty Domain Model and Difficulty DTO
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
