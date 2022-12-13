﻿using CaveManager.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CaveManager.Repository.Repository.Contract
{
    public interface IOwnerRepository
    {
        Task<Owner> AddOwnerAsync(Owner owner);
        Task<Owner> SelectOwnerAsync(int idOwner);
        Task<Owner> UpdateOwnerAsync(int idOwner, Owner owner);
        Task<(string, bool)> UpdateOwnerPasswordAsync(int idOwner, string password);
        Task<Owner> DeleteOwnerAsync(int idOwner);
        Task<Owner> RetrieveOwnerByPasswordAndLoginAsync(string email, string password);
        Task<List<Cave>> DeleteCaveAsync(int idOwner);
        Task<bool> CheckAgeAsync(DateTime birthDate);
        Task<List<Wine>> GetAllWineFromOwnerAsync(int idOwner);
        Task<List<Wine>> GetAllPeakWineFromOwnerAsync(int idOwner);
        Task<List<Wine>> AllDataForOwnerAsync(int idOwner);
    }
}
