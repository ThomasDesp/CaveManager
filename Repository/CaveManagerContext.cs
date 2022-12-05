using CaveManager.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;

namespace CaveManager.Repository
{
    public class CaveManagerContext : DbContext
    {
        public DbSet<Cave> Cave { get; set; }
        public DbSet<Wine> Wine { get; set; }
        public DbSet<Drawer> Drawer { get; set; }

        public CaveManagerContext(DbContextOptions<CaveManagerContext> option)
            : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluent API


            // default data


            var cave1 = new Cave { Id = 1, Name = "BatCave" };
            var cave2 = new Cave { Id = 2, Name = "ThomCave" };
            var cave3 = new Cave { Id = 3, Name = "Cavaleo" };

            var drawer1 = new Drawer { Id = 1, Name = "Pomme", MaxPlace = 10, PlaceUsed = 0, IdCave = 1 };
            var drawer2 = new Drawer { Id = 2, Name = "Poire", MaxPlace = 10, PlaceUsed = 0, IdCave = 2 };
            var drawer3 = new Drawer { Id = 3, Name = "Banana", MaxPlace = 10, PlaceUsed = 0, IdCave = 1 };

            var Wine1 = new Wine { Id = 1, Name = "Vin de fou", Type = "Red Wine" };
            var Wine2 = new Wine { Id = 2, Name = "Vin pas fou", Type = "Rosé Wine" };
            var Wine3 = new Wine { Id = 3, Name = "Vin de fou pas fou", Type = "White Wine" };




            modelBuilder.Entity<Cave>().HasData(new List<Cave> { cave1, cave1, cave1 });
            modelBuilder.Entity<Drawer>().HasData(new List<Drawer> { drawer1, drawer2, drawer3 });
            modelBuilder.Entity<Wine>().HasData(new List<Wine> { Wine1, Wine2, Wine3 });


            base.OnModelCreating(modelBuilder);
        }

    }
}
