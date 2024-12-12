using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext //from Microsoft.EntityFrameworkCore
    {
        //pass db options as parameter to a constructor because later we want to send our own connections through the Program.cs file
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
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
    }
}
