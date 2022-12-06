using CaveManager.Entities;
using CaveManager.Repository.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Xml.Linq;

namespace CaveManager.Repository
{
    public class WineRepository : IWine
    {
        CaveManagerContext context;
        ILogger<WineRepository> logger;
        public WineRepository(CaveManagerContext context, ILogger<WineRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        /// <summary>
        /// Add Wine to a Drawer
        /// </summary>
        /// <param name="wine"></param>
        /// <returns></returns>
        public async Task<Wine> AddWineAsync(Wine wine)
        {
            var addWine = context.Wine.Add(wine);
            await context.SaveChangesAsync();
            return wine;
        }

        /// <summary>
        /// Get a wine by his id 
        /// </summary>
        /// <param name="idWine"></param>
        /// <returns></returns>
        public async Task<Wine> SelectWineAsync(int idWine)
        {
            return await context.Wine.FindAsync(idWine);
        }

        /// <summary>
        /// Update a wine by his id
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<Wine> UpdateWineAsync(int idWine, string name, string type, string designation, int minVintageRecommended, int maxVintageRecommended)
        { 
            Wine wineUpdate = await context.Wine.FirstOrDefaultAsync(w => w.Id == idWine);
            wineUpdate.Name = name;
            wineUpdate.Type = type;
            wineUpdate.Designation = designation;
            wineUpdate.MinVintageRecommended = minVintageRecommended;
            wineUpdate.MaxVintageRecommended = maxVintageRecommended;
            wineUpdate.MaxVintageRecommended = maxVintageRecommended;
            //wineUpdate.IdDrawerPlace = idDrawerPlace;

            await context.SaveChangesAsync();
            return wineUpdate;
        }

        /// <summary>
        /// Remove wine from a Drawer with his id
        /// </summary>
        /// <param name="idWine"></param>
        /// <returns></returns>
        public async Task<bool> RemoveWineAsync(int idWine)
        {
            var deleteComment = await context.Wine.Where(w => w.Id == idWine).SingleOrDefaultAsync();
            context.Wine.Remove(deleteComment);
            context.SaveChanges();
            return true;
        }

        //public async Task<bool> ChangeWinePlaceAsync(Wine wine, int idDrawer)
        //{



        //}

    }
}
