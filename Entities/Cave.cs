﻿using System.ComponentModel.DataAnnotations.Schema;
namespace CaveManager.Entities
{
    
    [Table("Cave")]
    [Serializable]
    public class Cave
    {

        public Cave()
        {

        }
        public int Id { get; set; }

        public string Name { get; set; }
        public int IdOwner { get; set; }

        public List<Drawer> Drawer { get; set; }
    }
    
}
