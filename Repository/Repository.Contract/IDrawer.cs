using CaveManager.Entities;

namespace CaveManager.Repository.Repository.Contract
{
    public interface IDrawer
    {
        Task<Drawer> AddDrawerAsync(Drawer drawer);
        Task<Drawer> SelectDrawerAsync(int idDrawer);
        Task<List<Drawer>> GetAllDrawerFromACave(int idCave);
        Task<Drawer> UpdateDrawerAsync(int idDrawer, string Name, int MaxPlace, int PlaceUsed);
        Task<bool> RemoveDrawerAsync(int idDrawer);
    }
}
