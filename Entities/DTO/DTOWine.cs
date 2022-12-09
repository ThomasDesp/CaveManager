namespace CaveManager.Entities.DTO
{
    public class DTOWine
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Designation { get; set; }
        public int Bottling { get; set; }
        public int MinVintageRecommended { get; set; }
        public int MaxVintageRecommended { get; set; }
    }
}
