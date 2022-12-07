using CaveManager.Entities;

namespace CaveManager.Repository.Repository.Contract
{
    public interface IOwner
    {
        Task<Owner> AddOwnerAsync(Owner owner);
        Task<Owner> SelectOwnerAsync(int idOwner);
        Task<Owner> UpdateOwnerAsync(int idOwner, string firstname, string lastname, string email, string adress, string phoneNumber1, string phoneNumber2, string phoneNumber3);
        Task<Tuple<string, bool>> UpdateOwnerPasswordAsync(int idOwner, string password);
        Task<bool> DeleteOwnerAsync(int idOwner);
        Task<Owner> RetrieveOwnerByPasswordAndLoginAsync(string email, string password);
        Task<bool> DeleteCaveAsync(int idOwner);
        Task<bool> AllDataForOwnerAsync(int idOwner);
    }
}
