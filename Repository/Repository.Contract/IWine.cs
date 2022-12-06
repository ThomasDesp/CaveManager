using CaveManager.Entities;

namespace CaveManager.Repository.Repository.Contract
{
    public interface IWine
    {
        Task<Wine> AddWineAsync(Wine wine, int idDrawer);
        Task<Wine> GetWineAsync(int idWine);
        Task<List<Wine>> GetAllWinesFromADrawerAsync(int idDrawer);
        Task<Wine> PutWineAsync(int idWine, string name, string type, string designation, int minVintageRecommended, int maxVintageRecommended);
        Task<bool> DeleteWineAsync(int idWine);
    }
}
