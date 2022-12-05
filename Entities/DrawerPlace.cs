using System.ComponentModel.DataAnnotations.Schema;
namespace CaveManager.Entities
{
    [Table("DrawerPlace")]
    public class DrawerPlace
    {

        public int Id { get; set; }
        public int MaxPlace { get; set; }
        public int PlaceUsed { get; set; }
        public int IdDrawer { get; set; }
        public List<Wine> Wines { get; set; }

        public DrawerPlace(){

        }


    }
}
