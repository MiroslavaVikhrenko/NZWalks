using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        //definitions of the methods
        Task<Walk> CreateAsync(Walk walk); //repositories use domain models
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true); //nullabale parameters, by default null
        Task<Walk?> GetByIdAsync(Guid id); //Walk could be null (whether this walk is present in the db or not)
        Task<Walk?> UpdateAsync(Guid id, Walk walk); //Walk is nullable
        Task<Walk?> DeleteAsync(Guid id); //Walk is nullable
    }
}
