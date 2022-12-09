using CaveManager.Entities;
using System.Threading.Tasks;

namespace CaveManager.Repository.Repository.Contract
{
    public interface IWineRepository
    {
        Task<Wine> AddWineAsync(Wine wine, int idDrawer);
        Task<Wine> GetWineAsync(int idWine);
        Task<Wine> PutWineAsync(Wine wine , int idWine);
        Task<Wine> DeleteWineAsync(int idWine);
        Task<Wine> DuplicateWineAsync(int idWine, int idDrawer);
    }
}
