using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext //from Microsoft.EntityFrameworkCore
    {
        //pass db options as parameter to a constructor because later we want to send our own connections through the Program.cs file
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions) 
        {
            
        }

        //create db sets

        /*A Db set is a property of db context class that represents a collection of entities in the db
        In our app we have 3 entities or domain models and they are: Difficulty, Region, Walk.
        We want to represent the sets of each of these entities as a collection in our db and we will do that inside the db context.
        So we will create properties for each of them */

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions{ get; set; }
        public DbSet<Walk> Walks { get; set; }

        //data seeding through EF Core
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Difficulties
            //Easy, Medium, Hard

            var difficulties = new List<Difficulty>() //create a new list of Difficulty Domain Models
            {
                new Difficulty()
                {
                    //we cannot use Guid.NewGuid() to populate Id because that will change the value every time we run EF Core migrations
                    //so, I will create 3 GUID indentifiers for the difficulties using C# Interactive window
                    Id = Guid.Parse("d7219cdc-4e85-417d-97c6-93783b9ac5d5"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("46bc4828-7685-488d-b826-5ed16efbca6c"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("5ec8e717-d5be-4a45-b71f-01cc9820eee5"),
                    Name = "Hard"
                }
            };

            //seed difficulties to the db
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed data for Regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("a0630efb-f69d-4329-9511-232f47c3784f"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id = Guid.Parse("f96449cf-dc46-4cf9-9f25-b4b47100f799"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("68547f6d-8c96-4ba4-b1ac-ae95ea8316b9"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("0777347e-0ead-472f-92c9-710d8202d862"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id = Guid.Parse("895ae5a8-a93a-4a56-a2ed-98496cdeb148"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id = Guid.Parse("f1856a5e-28ed-42ef-b186-29cd10a40238"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
