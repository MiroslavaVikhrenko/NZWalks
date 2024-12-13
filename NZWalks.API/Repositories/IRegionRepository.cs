using NZWalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        //definitions of the 5 methods that we want to expose and later implement in a concrete class
        Task<List<Region>> GetAllAsync(); //returns a list of region domain models

        Task<Region?> GetByIdAsync(Guid id); //Region can be null
        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id, Region region); //Region can be null
        Task<Region?> DeleteAsync(Guid id); //Region can be null
    }
}
