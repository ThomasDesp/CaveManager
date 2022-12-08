namespace CaveManager.Entities.DTO
{
    public class DTOWine
    {
        public int IdWine { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Designation { get; set; }
        public int? MinVintageRecommended { get; set; }
        public int? MaxVintageRecommended { get; set; }
    }
}
