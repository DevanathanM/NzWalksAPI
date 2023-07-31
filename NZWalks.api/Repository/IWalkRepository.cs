using NZWalks.api.Models.Domain;

namespace NZWalks.api.Repository
{
    public interface IWalkRepository
    {
        Task<Walk?> CreateWalkAsync(Walk walk);

        Task<Walk?> UpdateWalkAsync(Guid Id, Walk walk);

        Task<Walk?> DeleteWalkAsync(Guid Id);

        Task<Walk> GetWalkByIdAsync(Guid Id);
        Task<List<Walk?>> GetWalkAsync(string? filteron = null, string? filterquery = null, string? sortby = null, bool isAscending= true);
    }
}
