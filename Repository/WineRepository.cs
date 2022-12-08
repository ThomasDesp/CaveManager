using CaveManager.Entities;
using CaveManager.Repository.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

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
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        public async Task<Wine> AddWineAsync(Wine wine, int idDrawer)
        {
            wine.DrawerId = idDrawer;
            var addWine =await context.Wine.AddAsync(wine);
            await context.SaveChangesAsync();
            return wine;
        }

        /// <summary>
        /// Get a wine by his id 
        /// </summary>
        /// <param name="idWine"></param>
        /// <returns></returns>
        public async Task<Wine> GetWineAsync(int idWine)
        {
            return await context.Wine.FindAsync(idWine);
        }

        /// <summary>
        /// Get wines by his drawer's id 
        /// </summary>
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        public async Task<List<Wine>> GetAllWinesFromADrawerAsync(int idDrawer)
        {
            return await context.Wine.Where(w => w.Id == idDrawer).ToListAsync();
        }

        /// <summary>
        /// Update a wine by his id
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<Wine> PutWineAsync(int idWine, string name, string type, string designation, int minVintageRecommended, int maxVintageRecommended)
        { 
            Wine wineUpdate = await context.Wine.FirstOrDefaultAsync(w => w.Id == idWine);
            wineUpdate.Name = name;
            wineUpdate.Type = type;
            wineUpdate.Designation = designation;
            wineUpdate.MinVintageRecommended = minVintageRecommended;
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
        public async Task<bool> DeleteWineAsync(int idWine)
        {
            var deleteWine = await context.Wine.Where(w => w.Id == idWine).SingleOrDefaultAsync();
            context.Wine.Remove(deleteWine);
            await context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Duplicate wine and add it to a specific drawer
        /// </summary>
        /// <param name="idWine"></param>
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        public async Task<bool> DuplicateWineAsync(int idWine, int idDrawer)
        {
            var duplicateWine = await context.Wine.Where(w => w.Id == idWine).SingleOrDefaultAsync();
            var name = duplicateWine.Name ;
            var type = duplicateWine.Type;
            var designation = duplicateWine.Designation;
            var minVintageRecommended = duplicateWine.MinVintageRecommended;
            var maxVintageRecommended = duplicateWine.MaxVintageRecommended;
            Wine wine = new Wine { Name = name, Type = type, Designation = designation, MinVintageRecommended = minVintageRecommended, MaxVintageRecommended = maxVintageRecommended };

            await AddWineAsync(wine, idDrawer);
            await context.SaveChangesAsync();
            return true;
        }

        //public async Task<bool> ChangeWinePlaceAsync(Wine wine, int idDrawer)
        //{



        //}

    }
}
