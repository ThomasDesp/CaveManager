﻿using CaveManager.Repository.Repository.Contract;
using CaveManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaveManager.Repository
{
    public class CaveRepository : ICave
    {
        CaveManagerContext context;
        ILogger<CaveRepository> logger;
        public CaveRepository(CaveManagerContext context, ILogger<CaveRepository> logger)
        {
            this.context = context;
            this.logger = logger;
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
        public async Task<bool> RemoveCaveAsync(int idCave)
        {
            var deleteCave = await context.Cave.Where(w => w.Id == idCave).SingleOrDefaultAsync();
            context.Cave.Remove(deleteCave);
            context.SaveChanges();
            return true;
        }
    }
}
