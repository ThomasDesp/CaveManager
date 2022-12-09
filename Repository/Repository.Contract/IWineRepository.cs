using CaveManager.Entities;
using System.Threading.Tasks;

namespace CaveManager.Repository.Repository.Contract
{
    public interface IWineRepository
    {
        Task<Wine> AddWineAsync(Wine wine, int idDrawer);
        Task<Wine> GetWineAsync(int idWine);
        Task<Wine> PutWineAsync(int idWine, string name, string type, string designation, int minVintageRecommended, int maxVintageRecommended);
        Task<bool> DeleteWineAsync(int idWine);
        Task<bool> DuplicateWineAsync(int idWine, int idDrawer);
    }
}
