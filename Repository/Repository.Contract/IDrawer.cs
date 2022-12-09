﻿using CaveManager.Entities;
using CaveManager.Entities.DTO;

namespace CaveManager.Repository.Repository.Contract
{
    public interface IDrawer
    {
        Task<Drawer> AddDrawerAsync(Drawer drawer);
        Task<Drawer> SelectDrawerAsync(int idDrawer);
        Task<List<Drawer>> GetAllDrawerFromACave(int idCave);
        Task<Drawer> UpdateDrawerAsync(DTODrawer dTODrawer);
        Task<bool> RemoveDrawerAsync(int idDrawer);
        Task<bool> RemoveAllWineAsync(int idDrawer);

    }
}
