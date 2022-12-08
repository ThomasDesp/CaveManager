using CaveManager.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using CaveManager.Repository.Repository.Contract;

namespace CaveManager.Repository
{
    public class CaveManagerContext : DbContext
    {
        public DbSet<Cave> Cave { get; set; }
        public DbSet<Wine> Wine { get; set; }
        public DbSet<Drawer> Drawer { get; set; }
        public DbSet<Owner> Owner { get; set; }

        public CaveManagerContext(DbContextOptions<CaveManagerContext> option)
            : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cave1 = new Cave { Id = 1, OwnerId = 2,Name = "BatCave" };
            var cave2 = new Cave { Id = 2 , OwnerId = 2,Name = "ThomCave" };
            var cave3 = new Cave { Id = 3, OwnerId = 1, Name = "Cavaleo" };

            var drawer1 = new Drawer { Id = 1, Name = "Pomme", MaxPlace = 10, PlaceUsed = 2, CaveId = 1 };
            var drawer2 = new Drawer { Id = 2, Name = "Poire", MaxPlace = 10, PlaceUsed = 1, CaveId = 2 };
            var drawer3 = new Drawer { Id = 3, Name = "Banana", MaxPlace = 10, PlaceUsed = 0, CaveId = 1 };

            var wine1 = new Wine { Id = 1, Name = "Vin de fou", Type = "Red Wine", DrawerId=1,Bottling=2019,MaxVintageRecommended=8,MinVintageRecommended=2 };
            var wine2 = new Wine { Id = 2, Name = "Vin pas fou", Type = "Rosé Wine", DrawerId=1,Bottling=2011, MaxVintageRecommended = 8, MinVintageRecommended = 4 };
            var wine3 = new Wine { Id = 3, Name = "Vin de fou pas fou", Type = "White Wine", DrawerId=2,Bottling=2012, MaxVintageRecommended = 12, MinVintageRecommended = 10 };

            var owner1 = new Owner { Id = 1, FirstName = "Wil", LastName="TF" , Email="wil@gmail.com", Password = "MelmanoucheA9", IsCGUAccepted = true };
            var owner2 = new Owner { Id = 2, FirstName = "Leo", LastName = "SMaster", Email = "leo@gmail.com", Password = "1v9A", IsCGUAccepted = true };
            var owner3 = new Owner { Id = 3, FirstName = "Thom", LastName = "PokFan", Email = "thom@gmail.com", Password = "DAzE2", IsCGUAccepted = false };

            modelBuilder.Entity<Owner>().HasData(new List<Owner> { owner1, owner2, owner3 });
            modelBuilder.Entity<Cave>().HasData(new List<Cave> { cave1, cave2, cave3 });
            modelBuilder.Entity<Drawer>().HasData(new List<Drawer> { drawer1, drawer2, drawer3 });
            modelBuilder.Entity<Wine>().HasData(new List<Wine> { wine1, wine2, wine3 });
            

            base.OnModelCreating(modelBuilder);
        }

    }
}
