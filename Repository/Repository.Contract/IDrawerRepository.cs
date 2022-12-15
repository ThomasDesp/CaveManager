using CaveManager.Entities;
using CaveManager.Entities.DTO;

namespace CaveManager.Repository.Repository.Contract
{
    public interface IDrawerRepository
    {
        Task<Drawer> AddDrawerAsync(Drawer drawer, int idCave);
        Task<Drawer> SelectDrawerAsync(int idDrawer);
        Task<List<Wine>> GetAllWinesFromADrawerAsync(int idDrawer);
        Task<(Drawer drawer, string error)> UpdateDrawerAsync(int idDrawer, Drawer drawer);
        Task<Drawer> RemoveDrawerAsync(int idDrawer);
        Task<List<Wine>> RemoveAllWineAsync(int idDrawer);

    }
}
