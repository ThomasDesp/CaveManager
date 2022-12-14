using CaveManager.Repository.Repository.Contract;
using CaveManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaveManager.Repository
{
    public class CaveRepository : ICaveRepository
    {
        IDrawerRepository drawerRepository;
        CaveManagerContext context;
        ILogger<CaveRepository> logger;
        public CaveRepository(CaveManagerContext context, ILogger<CaveRepository> logger, IDrawerRepository drawerRepository)
        {
            this.context = context;
            this.logger = logger;
            this.drawerRepository = drawerRepository;
        }

        /// <summary>
        /// Add a cave
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
        /// Get a cave by his id 
        /// </summary>
        /// <param name="idcave"></param>
        /// <returns></returns>
        public async Task<Cave> SelectCaveAsync(int idCave)
        {
            return await context.Cave.FindAsync(idCave);
        }

        /// <summary>
        /// Select All Cave of a Owner with his id
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        public async Task<List<Cave>> GetAllCaveFromAOwner(int ownerId)
        {
            return await context.Cave.Include(c => c.Drawer).ThenInclude(c => c.Wines).Where(w => w.OwnerId == ownerId).ToListAsync();
        }

        /// <summary>
        /// Select all Drawer of a Cave with his id
        /// </summary>
        /// <param name="idCave"></param>
        /// <returns></returns>
        public async Task<List<Drawer>> GetAllDrawerFromACave(int idCave)
        {
            return await context.Drawer.Where(w => w.CaveId == idCave).ToListAsync();
        }

        /// <summary>
        /// Update a owner by his id 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<Cave> UpdateCaveAsync(int Id, string Name)
        {
            Cave caveUpdate = await context.Cave.FirstOrDefaultAsync(u => u.Id == Id);
            if (caveUpdate != null) 
            {
                caveUpdate.Name = Name;
                await context.SaveChangesAsync();
            }
            return caveUpdate;
        }

        /// <summary>
        /// Remove a cave with his id
        /// </summary>
        /// <param name="idcave"></param>
        /// <returns></returns>
        public async Task<Cave> RemoveCaveAsync(int idCave)
        {
            var deleteCave = await context.Cave.Where(w => w.Id == idCave).SingleOrDefaultAsync();
            if (deleteCave != null)
            {
                await RemoveAllDrawerAsync(idCave);
                context.Cave.Remove(deleteCave);
                await context.SaveChangesAsync();
                return deleteCave;
            }
            else
                return deleteCave;

        }
        /// <summary>
        /// Remove all Drawer with an idCave
        /// </summary>
        /// <param name="idCave"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAllDrawerAsync(int idCave)
        {
            var deleteDrawer = await context.Drawer.Where(w => w.CaveId == idCave).ToListAsync();
            foreach (var item in deleteDrawer)
            {
                await drawerRepository.RemoveDrawerAsync(item.Id);
            }
            return true;
        }
    }
}
