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
        public async Task<Wine> AddWine(Wine wine)
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
        public async Task<Wine> SelectWine(int idWine)
        {
            return await context.Wines.FindAsync(idWine);
        }

        /// <summary>
        /// Update a wine by his id
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<Wine> UpdateWine(int idWine, string name, string type, string designation, int minVintageRecommended, int maxVintageRecommended,)
        { 
            Wine t = await context.Wines.FirstOrDefaultAsync(w => w.Id == idWine);
            t.Name = name;
            t.Type = type;
            t.Designation = designation;
            t.MinVintageRecommended = minVintageRecommended;
            t.MaxVintageRecommended = maxVintageRecommended;
            //t.IdDrawerPlace = idDrawerPlace;

            await context.SaveChangesAsync();
            return t;
        }

        /// <summary>
        /// Remove wine from a Drawer with his id
        /// </summary>
        /// <param name="idWine"></param>
        /// <returns></returns>
        public async Task<bool> RemoveWine(int idWine)
        {
            var deleteComment = await context.Wines.Where(w => w.Id == idWine).SingleOrDefaultAsync();
            context.Comments.Remove(deleteComment);
            context.SaveChanges();
            return true;
        }

        public async Task<bool> ChangeWinePlace(Wine wine, int idDrawer)
        {



        }

    }
}
