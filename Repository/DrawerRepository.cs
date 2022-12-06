using CaveManager.Entities;
using CaveManager.Repository.Repository.Contract;
using Microsoft.EntityFrameworkCore;


namespace CaveManager.Repository
{
    public class DrawerRepository : IDrawer
    {


        DrawerManagerContext context;
            ILogger<DrawerRepository> logger;
            public DrawerRepository(DrawerManagerContext context, ILogger<DrawerRepository> logger)
            {
                this.context = context;
                this.logger = logger;
            }

        /// <summary>
        /// Add an cave
        /// </summary>
        /// <param name="Drawer"></param>
        /// <returns></returns>
        public async Task<Drawer> AddDrawerAsync(Drawer drawer)
            {
                var addDrawer = context.Drawer.Add(drawer);
                await context.SaveChangesAsync();
                return drawer;
            }

            /// <summary>
            /// Get an drawer by his id 
            /// </summary>
            /// <param name="iddrawer"></param>
            /// <returns></returns>
            public async Task<Drawer> SelectDrawerAsync(int idDrawer)
            {
                return await context.Drawer.FindAsync(idDrawer);
            }

            /// <summary>
            /// Update an Drawer by his id
            /// </summary>
            /// <param name=""></param>
            /// <returns></returns>
        public async Task<Drawer> UpdateDrawerAsync(int Id, string Name, int MaxPlace, int PlaceUsed ,int IdCave)
            {
                Drawer drawerUpdate = await context.Cave.FirstOrDefaultAsync(u => u.Id == IdDrawer);
                drawerUpdate.Name = Name;
                drawerUpdate.IdCave = IdCave;
                drawerUpdate.MaxPlace = MaxPlace;
                drawerUpdate.PlaceUsed = PlaceUsed;
            //drawerUpdate.

            await context.SaveChangesAsync();
                return drawerUpdate;
            }

            /// <summary>
            /// Remove an drawer with his id
            /// </summary>
            /// <param name="iddrawer"></param>
            /// <returns></returns>
            public async Task<bool> RemoveDrawerAsync(int idDrawer)
            {
                var deleteDrawer = await context.Drawer.Where(w => w.Id == idDrawer).SingleOrDefaultAsync();
                context.Drawer.Remove(deleteDrawer);
                context.SaveChanges();
                return true;
            }
    }
}
