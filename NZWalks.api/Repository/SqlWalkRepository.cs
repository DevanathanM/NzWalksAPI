using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.api.Data;
using NZWalks.api.Models.Domain;
using System.Net.Http.Headers;

namespace NZWalks.api.Repository
{
    public class SqlWalkRepository:IWalkRepository
    {
        public readonly NZWalksDBContext dBContext;

        public SqlWalkRepository(NZWalksDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Walk> CreateWalkAsync(Walk Walk)
        {
            await dBContext.Walks.AddAsync(Walk);
            await dBContext.SaveChangesAsync();
            return Walk;
        }

        public async Task<List<Walk>> GetWalkAsync(string? filteron = null, string? filterquery = null, string? sortby = null, bool isAscending = true)
        {
            var walk= dBContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            if(string.IsNullOrWhiteSpace(filteron) == false && string.IsNullOrWhiteSpace(filterquery) == false )
            {
                if(filteron.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walk= walk.Where(x => x.Name.Contains(filterquery));
                }
            }

            if (string.IsNullOrWhiteSpace(sortby) == false )
            {
                if (sortby.Equals("Name", StringComparison.OrdinalIgnoreCase) )
                {
                    walk = isAscending ? walk.OrderBy(x => x.Name) : walk.OrderByDescending(x => x.Name);
                        
                }
            }

            return await walk.ToListAsync();
            
        }

        public async Task<Walk> GetWalkByIdAsync(Guid Id)
        {
           return await dBContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == Id);
            
        }

        public async Task<Walk> UpdateWalkAsync(Guid Id, Walk walk)
        {
            var walkId= await dBContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);

          if(walkId != null)
            {
                walkId.Name = walk.Name;
                walkId.Description = walk.Description;
                walkId.WalkimageURL = walk.WalkimageURL;
                walkId.DifficultyId = walk.DifficultyId;
                walkId.RegionId  = walk.RegionId;
                walkId.LengthInKm   = walk.LengthInKm;

            }

            await dBContext.SaveChangesAsync();
            return walk;


                
        }

        public async Task<Walk?> DeleteWalkAsync(Guid Id)
        {
            var DeleteWalkId = await dBContext.Walks.FirstOrDefaultAsync(x=>x.Id == Id);

            if(DeleteWalkId == null)
            {
                return null;
            }
                
            dBContext.Walks.Remove(DeleteWalkId);
            await dBContext.SaveChangesAsync();
            return DeleteWalkId;
        }
    }
}
