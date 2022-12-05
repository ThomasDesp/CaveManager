using CaveManager.Entities;
using CaveManager.Repository.Repository.Contract;
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
        /// Eemove wine from a Drawer with his id
        /// </summary>
        /// <param name="idWine"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCommentAsync(int idWine)
        {
            var deleteComment = await context.Wines.Where(c => c.Id == idWine).SingleOrDefaultAsync();
            context.Comments.Remove(deleteComment);
            context.SaveChanges();
            return true;
        }

    }
}
