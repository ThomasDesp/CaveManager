using CaveManager.Entities;

namespace CaveManager.Repository.Repository.Contract
{
    public interface IOwner
    {
        Task<Owner> AddOwnerAsync(Owner owner);
        Task<Owner> SelectOwnerAsync(int idOwner);
        Task<Owner> UpdateOwnerAsync(int idOwner, string firstname, string lastname, string email, string adress);
        Task<Tuple<string, bool>> UpdateOwnerPasswordAsync(int idOwner, string password);
        Task<bool> RemoveOwnerAsync(int idOwner);
        Task<Owner> RetrieveOwnerByPasswordAndLoginAsync(string email, string password);
    }
}
