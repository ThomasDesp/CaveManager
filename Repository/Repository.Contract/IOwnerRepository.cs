using CaveManager.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CaveManager.Repository.Repository.Contract
{
    public interface IOwnerRepository
    {
        Task<Owner> AddOwnerAsync(string firstname, string lastname, string password, string email, string address, string phoneNumber1, string phoneNumber2, string phoneNumber3);
        Task<Owner> SelectOwnerAsync(int idOwner);
        Task<Owner> UpdateOwnerAsync(int idOwner, string firstname, string lastname, string email, string address, string phoneNumber1, string phoneNumber2, string phoneNumber3);
        Task<(string, bool)> UpdateOwnerPasswordAsync(int idOwner, string password);
        Task<bool> DeleteOwnerAsync(int idOwner);
        Task<Owner> RetrieveOwnerByPasswordAndLoginAsync(string email, string password);
        Task<bool> DeleteCaveAsync(int idOwner);
        Task<bool> CheckAgeAsync(DateTime birthDate);
        Task<List<Wine>> GetAllWineFromOwnerAsync(int idOwner);
        Task<List<Wine>> GetAllPeakWineFromOwnerAsync(int idOwner);
        Task<bool> AllDataForOwnerAsync(int idOwner);
    }
}
