namespace CaveManager.Repository.Repository.Contract
{
    public interface IDrawer
    {
        Task<Drawer> AddDrawerAsync(Drawer drawer);
        Task<Drawer> SelectDrawerAsync(int idDrawer);
        Task<Drawer> UpdateDrawerAsync(int idDrawer, int MaxPlace, int PlaceUsed, int IdCave);
        Task<bool> RemoveDrawerAsync(int idDrawer);
    }
}
