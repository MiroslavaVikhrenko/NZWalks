using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        //definitions of the methods
        Task<Walk> CreateAsync(Walk walk); //repositories use domain models
    }
}
