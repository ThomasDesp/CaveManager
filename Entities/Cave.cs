using System.ComponentModel.DataAnnotations.Schema;
namespace CaveManager.Entities
{
    
   [Table("Cave")]
    public class Cave
    {

        public Cave()
        {

        }
        public int Id { get; set; }

        public string Name { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }

        public List<Drawer> Drawer { get; set; }
    }
    
}
