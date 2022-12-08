using System.ComponentModel.DataAnnotations.Schema;

namespace CaveManager.Entities
{
    [Table("Wine")]
    [Serializable]
    public class Wine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Designation { get; set; }
        public int? MinVintageRecommended { get; set; }
        public int? MaxVintageRecommended { get; set; }
        public int Bottling { get; set; }
        public int DrawerId { get; set; }
        public Drawer Drawer { get; set; }
        public Wine()
        {
        }
    }
}
