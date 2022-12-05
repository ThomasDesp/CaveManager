﻿using System.ComponentModel.DataAnnotations.Schema;
namespace CaveManager.Entities
{
    [Table("Drawer")]
    public class Drawer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCave { get; set; }
        public List<DrawerPlace> DrawerPlaces {get; set;}
    }
}
