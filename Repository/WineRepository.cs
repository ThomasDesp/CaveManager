using CaveManager.Entities;
using CaveManager.Repository.Repository.Contract;
using System.ComponentModel;
using System.Xml.Linq;

namespace CaveManager.Repository
{
    public class WineRepository : IWineRepository
    {
        //WikyContext context;
        //ILogger<WineRepository> logger;
        //public WineRepository(WikyContext context, ILogger<WineRepository> logger)
        //{
        //    this.context = context;
        //    this.logger = logger;
        //}

        /// <summary>
        /// Add Wine to a Drawer
        /// </summary>
        /// <param name="wine"></param>
        /// <returns></returns>
        public async Task<Wine> AddWineAsync(Wine wine)
        {
            var addWine = context.Wines.Add(wine);
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
            return await context.Wines.FindAsync(idWine);
        }

        /// <summary>
        /// Update a wine by his id
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<Wine> UpdateWineAsync(int idWine, string name, string type, string designation, int minVintageRecommended, int maxVintageRecommended)
        { 
            Wine wineUpdate = await context.Wines.FirstOrDefaultAsync(w => w.Id == idWine);
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
            var deleteComment = await context.Wines.Where(w => w.Id == idWine).SingleOrDefaultAsync();
            context.Comments.Remove(deleteComment);
            context.SaveChanges();
            return true;
        }

        public async Task<bool> ChangeWinePlaceAsync(Wine wine, int idDrawer)
        {



        }

    }
}
