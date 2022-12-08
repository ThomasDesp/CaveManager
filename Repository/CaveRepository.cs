﻿using CaveManager.Repository.Repository.Contract;
using CaveManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaveManager.Repository
{
    public class CaveRepository : ICave
    {
        IDrawer drawerRepository;
        CaveManagerContext context;
        ILogger<CaveRepository> logger;
        public CaveRepository(CaveManagerContext context, ILogger<CaveRepository> logger, IDrawer drawerRepository)
        {
            this.context = context;
            this.logger = logger;
            this.drawerRepository = drawerRepository;
        }

        /// <summary>
        /// Add an cave
        /// </summary>
        /// <param name="cave"></param>
        /// <returns></returns>
        public async Task<Cave> AddCaveAsync(Cave cave)
        {
            var addCave = context.Cave.Add(cave);
            await context.SaveChangesAsync();
            return cave;
        }

        /// <summary>
        /// Get an cave by his id 
        /// </summary>
        /// <param name="idcave"></param>
        /// <returns></returns>
        public async Task<Cave> SelectCaveAsync(int idCave)
        {
            return await context.Cave.FindAsync(idCave);
        }

        public async Task<List<Cave>> GetAllCaveFromAOwner(int ownerId)
        {
            return await context.Cave.Include(c => c.Drawer).ThenInclude(c => c.Wines).Where(w => w.OwnerId == ownerId).ToListAsync();
        }

        /// <summary>
        /// Update an owner by his id 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<Cave> UpdateCaveAsync(int Id, string Name)
        {
            Cave caveUpdate = await context.Cave.FirstOrDefaultAsync(u => u.Id == Id);
            caveUpdate.Name = Name;
            //caveUpdate.

            await context.SaveChangesAsync();
            return caveUpdate;
        }

        /// <summary>
        /// Remove an cave with his id
        /// </summary>
        /// <param name="idcave"></param>
        /// <returns></returns>
        public async Task<Cave> RemoveCaveAsync(int idCave)
        {
            var deleteCave = await context.Cave.Where(w => w.Id == idCave).SingleOrDefaultAsync();
            if (deleteCave != null)
            {
                await RemoveAllDrawerAsync(idCave);
                context.Cave.Remove(deleteCave);
                await context.SaveChangesAsync();
                return deleteCave;
            }
            else
                return deleteCave;


        }
        public async Task<bool> RemoveAllDrawerAsync(int idCave)
        {
            var deleteDrawer = await context.Drawer.Where(w => w.CaveId == idCave).ToListAsync();
            foreach (var item in deleteDrawer)
            {
                await drawerRepository.RemoveDrawerAsync(item.Id);
            }
            return true;
        }
    }
}
