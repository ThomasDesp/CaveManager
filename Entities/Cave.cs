namespace CaveManager.Entities
{
    public class Cave
    {
        [Table("Cave")]
    public class Cave
    {

        public Cave()
        {

        }


        public int CaveID { get; set; }

        public string Name { get; set; }

        public List<Drawer> Drawer { get; set; }
    }
    }
}
