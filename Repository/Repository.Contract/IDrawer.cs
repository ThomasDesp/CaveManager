using CaveManager.Entities;
using CaveManager.Entities.DTO;

namespace CaveManager.Repository.Repository.Contract
{
    public interface IDrawer
    {
        Task<Drawer> AddDrawerAsync(Drawer drawer);
        Task<Drawer> SelectDrawerAsync(int idDrawer);
        Task<List<Wine>> GetAllWinesFromADrawerAsync(int idDrawer);
        Task<Drawer> UpdateDrawerAsync(int idDrawer, DTODrawer dTODrawer);
        Task<Drawer> RemoveDrawerAsync(int idDrawer);
        Task<List<Wine>> RemoveAllWineAsync(int idDrawer);

    }
}
