using System.ComponentModel.DataAnnotations.Schema;
namespace CaveManager.Entities
{
    [Table("Drawer")]
    [Serializable]
    public class Drawer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxPlace { get; set; }
        public int PlaceUsed { get; set; }
        public int CaveId { get; set; }
        public List<Wine>? Wines {get; set;}
        public Cave Cave { get; set; }
    }
}
