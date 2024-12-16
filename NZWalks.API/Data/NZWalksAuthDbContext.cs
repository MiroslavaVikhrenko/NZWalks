using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext //Microsoft.AspNetCore.Identity.EntityFrameworkCore
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
        }

        //seeding roles
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //creating a list of roles (IdentityRole comes from Microsoft.AspNetCore.Identity)
            //IdentityRole.Id is string, but we use guid converted to string
            var readerRoleId = "476684cc-6c57-4776-9e22-4b184f25bf76";
            var writerRoleId = "f18f944d-cb75-40ba-b996-a79e4aeebab8";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            //seed this list of roles inside the builder object
            builder.Entity<IdentityRole>().HasData(roles); //now when we run EF Core migrations
                                                           //it will see this data that we need to seed these two roles
                                                           //if the roles don't exist in the db => 
                                                           //this EF Core migration will add/seed this data into the db
        }
    }
}
