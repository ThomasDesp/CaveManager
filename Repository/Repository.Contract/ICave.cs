namespace CaveManager.Repository.Repository.Contract
{
    public interface ICave
    {
        Task<Cave> AddCaveAsync(Cave cave);
        Task<Cave> SelectCaveAsync(int idCave);
        Task<Cave> UpdateCaveAsync(int idCave, string name, int idUser);
        Task<bool> RemoveCaveAsync(int idCave);
    }
}
