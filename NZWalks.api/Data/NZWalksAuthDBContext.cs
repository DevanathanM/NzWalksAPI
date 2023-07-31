using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.api.Data
{
    public class NZWalksAuthDBContext : IdentityDbContext
    {
        public NZWalksAuthDBContext(DbContextOptions<NZWalksAuthDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "66ddf9e5-357f-4140-b182-4144859e72a0";
            var writerRoleId = "6c75f4d6-3e93-4636-b824-5f65999112e6";



            var roles = new List<IdentityRole>
            {
                new IdentityRole
                { 
                    Id = readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name= "Reader",
                    NormalizedName= "Reader".ToUpper()
                },
                new IdentityRole
                { 
                    Id = writerRoleId, 
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer", 
                    NormalizedName="Writer".ToUpper() 
                }
                
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
