using NZWalks.api.Models.Domain;

namespace NZWalks.api.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionsAsync();

        Task<Region?> GetRegionByIdAsync(Guid Id);
        Task<Region?> CreateRegionAsync(Region region);
        Task<Region?> UpdateRegionAsync(Guid Id, Region region);
        Task<Region?> DeleteRegionAsync(Guid Id);
    }
}
