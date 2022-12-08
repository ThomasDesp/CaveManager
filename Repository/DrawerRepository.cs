using CaveManager.Entities;
using CaveManager.Repository.Repository.Contract;
using Microsoft.EntityFrameworkCore;


namespace CaveManager.Repository
{
    public class DrawerRepository : IDrawer
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

        public async Task<List<Drawer>> GetAllDrawerFromACave(int idCave)
        {
            return await context.Drawer.Where(w => w.CaveId == idCave).ToListAsync();
        }

        public async Task<List<Wine>> GetAllWineFromOwnerAsync(int idOwner)
        {
            //var wines = await context.Wine.Include(w => w.Drawer).ThenInclude(d => d.Cave).Where(w => w.Drawer.IdCave == idCave && w.Bottling+w.MinVintageRecommended <= date && w.Bottling + w.MaxVintageRecommended >= date ).ToListAsync();
            var wines = await context.Wine.Include(w => w.Drawer).ThenInclude(d => d.Cave).ThenInclude(o => o.Owner)
                .Where(w => w.Drawer.Cave.OwnerId == idOwner).ToListAsync();
            return wines;
        }

        public async Task<List<Wine>> GetAllPeakWineFromOwnerAsync(int idOwner)
        {
            int date = DateTime.Now.Year;
            var wines = await context.Wine.Include(w => w.Drawer).ThenInclude(d => d.Cave).ThenInclude(o => o.Owner)
                .Where(w => w.Drawer.Cave.OwnerId == idOwner && w.Bottling + w.MinVintageRecommended <= date && w.Bottling
                + w.MaxVintageRecommended >= date).ToListAsync();
            return wines;
        }

        /// <summary>
        /// Update an Drawer by his id
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<Drawer> UpdateDrawerAsync(int Id, string Name, int MaxPlace, int PlaceUsed)
        {
            Drawer drawerUpdate = await context.Drawer.FindAsync(Id);
            drawerUpdate.Name = Name;
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
            await RemoveAllWineAsync(idDrawer);
            context.Drawer.Remove(deleteDrawer);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveAllWineAsync(int idDrawer)
        {
            var deleteWines = await context.Wine.Where(w => w.DrawerId == idDrawer).ToListAsync();
            foreach (var item in deleteWines)
            {
                context.Wine.Remove(item);
            }
            await context.SaveChangesAsync();
            return true;
        }
    }
}
