using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.api.Data;
using NZWalks.api.Models.Domain;

namespace NZWalks.api.Repository
{
    public class SqlRegionRepository:IRegionRepository 
    {
        public readonly NZWalksDBContext dBContext;
        //private readonly IRegionRepository regionRepository;

        public SqlRegionRepository(NZWalksDBContext dBContext )
        {
            this.dBContext = dBContext;
            
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await dBContext.Regions.ToListAsync();
            
        }

        public async Task<Region?> GetRegionByIdAsync(Guid Id)
        {
            return await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Region?> CreateRegionAsync(Region region)
        {
            await dBContext.Regions.AddAsync(region);
            await dBContext.SaveChangesAsync();
            return region;
        }
        public async Task<Region?> UpdateRegionAsync(Guid Id, Region region)
        {
            var existingRegion= await dBContext.Regions.FirstOrDefaultAsync(x=>x.Id == Id);

            if(existingRegion==null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageurl = region.RegionImageurl;

            return existingRegion;
        }

        public async Task<Region?> DeleteRegionAsync(Guid Id)
        {
            var existingRegion = await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingRegion == null)
            {
                return null;
            }

            dBContext.Regions.Remove(existingRegion);
            await dBContext.SaveChangesAsync();
            return existingRegion;
        }

    }
}
