namespace CaveManager.Repository.Repository.Contract;
using CaveManager.Entities;

public interface ICave
{
    Task<Cave> AddCaveAsync(Cave cave);
    Task<Cave> SelectCaveAsync(int idCave);
    Task<Cave> UpdateCaveAsync(int idCave, string name);
    Task<bool> RemoveCaveAsync(int idCave);
    Task<bool> RemoveAllDrawerAsync(int idCave);
}
