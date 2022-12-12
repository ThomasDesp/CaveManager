using CaveManager.Entities;
using CaveManager.Repository.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace CaveManager.Repository
{
    public class WineRepository : IWineRepository
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
        public async Task<(Wine wine, string error)> AddWineAsync(Wine wine, int idDrawer)
        {
            var drawer = await context.Drawer.FindAsync(idDrawer);
            if (drawer != null)
            {
                if (drawer.PlaceUsed < drawer.MaxPlace)
                {
                    wine.DrawerId = idDrawer;
                    var addWine = await context.Wine.AddAsync(wine);
                    if (addWine != null)
                    {
                        await context.SaveChangesAsync();
                        return (wine,"ok");
                    }
                    return (wine, "Incorect wine's data");
                }
                return (wine, "Drawer is full");
            }
            return (wine, "Drawer can't be found");
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
        /// Update a wine by his id
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<Wine> PutWineAsync(Wine wine, int idWine)
        {
            Wine wineUpdate = await context.Wine.FirstOrDefaultAsync(w => w.Id == idWine);
            if (wineUpdate != null)
            {
                wineUpdate.Name = wine.Name;
                wineUpdate.Type = wine.Type;
                wineUpdate.Designation = wine.Designation;
                wineUpdate.MinVintageRecommended = wine.MinVintageRecommended;
                wineUpdate.MaxVintageRecommended = wine.MaxVintageRecommended;
                await context.SaveChangesAsync();
            }
            return wineUpdate;
        }

        /// <summary>
        /// Remove wine from a Drawer with his id
        /// </summary>
        /// <param name="idWine"></param>
        /// <returns></returns>
        public async Task<Wine> DeleteWineAsync(int idWine)
        {
            var deleteWine = await context.Wine.Where(w => w.Id == idWine).SingleOrDefaultAsync();
            if (deleteWine != null)
            {
                context.Wine.Remove(deleteWine);
                await context.SaveChangesAsync();
            }

            return deleteWine;
        }

        /// <summary>
        /// Duplicate wine and add it to a specific drawer
        /// </summary>
        /// <param name="idWine"></param>
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        public async Task<(Wine wine, string error)> DuplicateWineAsync(int idWine, int idDrawer)
        {
            var duplicateWine = await context.Wine.Where(w => w.Id == idWine).SingleOrDefaultAsync();
            if (duplicateWine != null)
            {

                var name = duplicateWine.Name;
                var type = duplicateWine.Type;
                var designation = duplicateWine.Designation;
                var minVintageRecommended = duplicateWine.MinVintageRecommended;
                var maxVintageRecommended = duplicateWine.MaxVintageRecommended;
                Wine wine = new Wine { Name = name, Type = type, Designation = designation, MinVintageRecommended = minVintageRecommended, MaxVintageRecommended = maxVintageRecommended };

                var res =await AddWineAsync(wine, idDrawer);
                await context.SaveChangesAsync();
                return (res.wine, res.error);
            }
            return(duplicateWine,"Wine not found");
            
        }

        public async Task<Owner> RetrieveUserByPasswordAndLogin(string password, string email)
        {
            var zo = context.Owner.Where(u => u.Password == password && u.Email == email).FirstOrDefault();
            return zo;
        }




    }
}
