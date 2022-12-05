﻿using CaveManager.Entities;

namespace CaveManager.Repository.Repository.Contract
{
    public interface IWineRepository
    {
        Task<Wine> AddWineAsync(Wine wine);
        Task<Wine> SelectWineAsync(int idWine);
        Task<Wine> UpdateWineAsync(int idWine, string name, string type, string designation, int minVintageRecommended, int maxVintageRecommended);
        Task<Wine> RemoveWineAsync(int idWine);

    }
}
