using CaveManager.Entities;
using CaveManager.Entities.DTO;
using CaveManager.Repository.Repository.Contract;
using Microsoft.EntityFrameworkCore;


namespace CaveManager.Repository
{
    public class DrawerRepository : IDrawerRepository
    {


        CaveManagerContext context;
        ILogger<DrawerRepository> logger;
        public DrawerRepository(CaveManagerContext context, ILogger<DrawerRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        /// <summary>
        /// Add an cave
        /// </summary>
        /// <param name="drawer"></param>
        /// <param name="idCave"></param>
        /// <returns></returns>
        public async Task<Drawer> AddDrawerAsync(Drawer drawer, int idCave)
        {
            var cave = await context.Cave.FindAsync(idCave);
            if (cave != null)
            {
                var addDrawer = await context.Drawer.AddAsync(drawer);
                await context.SaveChangesAsync();
                return drawer;
            }
            return drawer;
        }

        /// <summary>
        /// Get an drawer by his id 
        /// </summary>
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        public async Task<Drawer> SelectDrawerAsync(int idDrawer)
        {
            return await context.Drawer.Where(d => d.Id == idDrawer).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Get wines by his drawer's id 
        /// </summary>
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        public async Task<List<Wine>> GetAllWinesFromADrawerAsync(int idDrawer)
        {
            return await context.Wine.Where(w => w.DrawerId == idDrawer).ToListAsync();
        }

        /// <summary>
        /// Update an Drawer by his id
        /// </summary>
        /// <param name="idDrawer"></param>
        /// <param name="drawer"></param>
        /// <returns></returns>
        public async Task<(Drawer drawer, string error)> UpdateDrawerAsync(int idDrawer, Drawer drawer)
        {
            Drawer drawerUpdate = await context.Drawer.FindAsync(idDrawer);
            if (drawerUpdate != null)
            {
                if (drawerUpdate.PlaceUsed < drawer.MaxPlace)
                {
                    drawerUpdate.Name = drawer.Name;
                    drawerUpdate.MaxPlace = drawer.MaxPlace;
                    drawerUpdate.PlaceUsed = drawer.PlaceUsed;
                    await context.SaveChangesAsync();
                    return (drawerUpdate,"ok");
                }
                return (drawerUpdate, "This drawer have more wine than this new max capacity");
            }
            return (drawerUpdate, "Drawer not found");
        }

        /// <summary>
        /// Remove an drawer with his id
        /// </summary>
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        public async Task<Drawer> RemoveDrawerAsync(int idDrawer)
        {
            var deleteDrawer = await context.Drawer.Where(w => w.Id == idDrawer).SingleOrDefaultAsync();
            if (deleteDrawer != null)
            {
                await RemoveAllWineAsync(idDrawer);
                context.Drawer.Remove(deleteDrawer);
                await context.SaveChangesAsync();
                return deleteDrawer;
            }
            
            return deleteDrawer;
        }
        /// <summary>
        /// Remove all wine of the drawer selected
        /// </summary>
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        public async Task<List<Wine>> RemoveAllWineAsync(int idDrawer)
        {
            var deleteWines = await context.Wine.Where(w => w.DrawerId == idDrawer).ToListAsync();
            if (deleteWines != null)
            {


                foreach (var item in deleteWines)
                {
                    context.Wine.Remove(item);
                }
                await context.SaveChangesAsync();
            }
            
            return deleteWines;
        }
    }
}
