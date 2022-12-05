namespace CaveManager.Entities
{
    public class Wine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Type { get; set; }
        public string? Designation { get; set; }
        public int? MinVintageRecommended { get; set; }
        public int? MaxVintageRecommended { get; set; }
        public int? IdDrawerPlace { get; set; }

        public Wine()
        {
        }
    }
}
