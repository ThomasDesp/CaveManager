namespace CaveManager.Repository.Repository.Contract;
using CaveManager.Entities;

public interface ICaveRepository
{
    Task<Cave> AddCaveAsync(Cave cave);
    Task<Cave> SelectCaveAsync(int idCave);
    Task<List<Drawer>> GetAllDrawerFromACave(int idCave);
    Task<Cave> UpdateCaveAsync(int idCave, string name);
    Task<Cave> RemoveCaveAsync(int idCave);
    Task<bool> RemoveAllDrawerAsync(int idCave);
    Task<List<Cave>> GetAllCaveFromAOwner(int ownerId);
}
