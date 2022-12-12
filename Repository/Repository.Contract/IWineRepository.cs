using CaveManager.Entities;
using System.Threading.Tasks;

namespace CaveManager.Repository.Repository.Contract
{
    public interface IWineRepository
    {
        Task<(Wine wine, string error)> AddWineAsync(Wine wine, int idDrawer);
        Task<Wine> GetWineAsync(int idWine);
        Task<Wine> PutWineAsync(Wine wine , int idWine);
        Task<Wine> DeleteWineAsync(int idWine);
        Task<(Wine wine, string error)> DuplicateWineAsync(int idWine, int idDrawer);
    }
}
