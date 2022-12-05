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
        public DbSet<DrawerPlace> DrawerPlace { get; set; }

        public CaveManagerContext(DbContextOptions<CaveManagerContext> option)
            : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluent API


            // default data
            var wine1 = new Wine
            {
                Id = 1,
                Name = "Ronflex"
            };

            var wine2 = new Wine
            {
                Id = 2,
                Name = "Dimoret",
            };

            var wine3 = new Wine
            {
                Id = 3,
                Name = "Artikodin",
            };

            var cave1 = new Cave { Id = 1, Name = "Blue" };
            var cave2 = new Cave { Id = 2, Name = "Cynthia"};
            var cave3 = new Cave { Id = 3, Name = "Red" };

            var Move1 = new Drawer { Id = 1, Name = "Tackle" };
            var Move2 = new Drawer { Id = 2, Name = "Ice punch" };
            var Move3 = new Drawer { Id = 3, Name = "Ice wind"};





            modelBuilder.Entity<Wine>().HasData(new List<Wine> { wine3, wine3, wine3 });
            modelBuilder.Entity<Cave>().HasData(new List<Cave> { cave1, cave1, cave1 });
            modelBuilder.Entity<Drawer>().HasData(new List<Drawer> { Move1, Move2, Move3 });

            base.OnModelCreating(modelBuilder);
        }

    }
}
