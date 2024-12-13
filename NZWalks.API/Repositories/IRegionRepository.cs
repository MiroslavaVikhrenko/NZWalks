using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        //definitions of the 5 methods that we want to expose and later implement in a concrete class
        Task<List<Region>> GetAllAsync(); //returns a list of region domain models

        Task<Region?> GetById(Guid id); //Region can be null
    }
}
