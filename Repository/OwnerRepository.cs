using CaveManager.Entities;
using CaveManager.Repository.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace CaveManager.Repository
{
    public class OwnerRepository : IOwner
    {
        ICave caveRepository;
        IDrawer drawerRepository;
        IWine wineRepository;
        CaveManagerContext context;
        ILogger<OwnerRepository> logger;
        public OwnerRepository(CaveManagerContext context, ILogger<OwnerRepository> logger, ICave caveRepository, IDrawer drawerRepository, IWine wineRepository)
        {
            this.context = context;
            this.logger = logger;
            this.caveRepository = caveRepository;
            this.drawerRepository = drawerRepository;
            this.wineRepository = wineRepository;
        }

        /// <summary>
        /// Add an owner
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public async Task<Owner> AddOwnerAsync(Owner owner)
        {
            var addOwner = context.Owner.Add(owner);
            await context.SaveChangesAsync();
            return owner;
        }

        /// <summary>
        /// Get an owner by his id 
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        public async Task<Owner> SelectOwnerAsync(int idOwner)
        {
            return await context.Owner.FindAsync(idOwner);
        }

        /// <summary>
        /// Update an owner by his id
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<Owner> UpdateOwnerAsync(int idOwner, string firstname, string lastname, string email, string adress, string phoneNumber1, string phoneNumber2, string phoneNumber3)
        {
            Owner ownerUpdate = await context.Owner.FirstOrDefaultAsync(o => o.Id == idOwner);
            ownerUpdate.FirstName = firstname;
            ownerUpdate.FullName = firstname + lastname;
            ownerUpdate.Email = email;
            ownerUpdate.Adress = adress;
            ownerUpdate.PhoneNumber1 = phoneNumber1;
            ownerUpdate.PhoneNumber2 = phoneNumber2;
            ownerUpdate.PhoneNumber3 = phoneNumber3;

            await context.SaveChangesAsync();
            return ownerUpdate;
        }

        /// <summary>
        /// Update owner's password
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> UpdateOwnerPasswordAsync(int idOwner, string password)
        {
            Owner ownerUpdate = await context.Owner.FirstOrDefaultAsync(o => o.Id == idOwner);
            if (ownerUpdate == null)
                return new Tuple<string, bool>("Owner Id dont exist", false);
            var passwordChecked = Owner.IsPasswordValidated(password);
            if (passwordChecked)
                ownerUpdate.Password = password;
            else
                return new Tuple<string, bool>("Password incorrect please retry", false);

            await context.SaveChangesAsync();
            return new Tuple<string, bool>("test", true);
        }



        /// <summary>
        /// Remove an owner with his id
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        public async Task<bool> DeleteOwnerAsync(int idOwner)
        {
            var deleteOwner = await context.Owner.Where(o => o.Id == idOwner).SingleOrDefaultAsync();
            context.Owner.Remove(deleteOwner);
            await context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Looking for an owner with email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Owner> RetrieveOwnerByPasswordAndLoginAsync(string email, string password)
        {
            return context.Owner.Where(o => o.Email == email && o.Password == password).FirstOrDefault();
        }

        /// <summary>
        /// Delete cave by idOwner
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCaveAsync(int idOwner)
        {
            var deleteCave = await context.Cave.Where(c => c.OwnerId == idOwner).SingleOrDefaultAsync();
            RemoveAllCaves(idOwner);
            context.Cave.Remove(deleteCave);
            await context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Remove all caves from an idOwner
        /// </summary>
        /// <param name="idCave"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAllCaves(int idCave)
        {
            var deleteCave = await context.Cave.Where(c => c.Id == idCave).ToListAsync();
            foreach (var item in deleteCave)
            {
                caveRepository.RemoveCaveAsync(item.Id);
            }
            return true;
        }

        /// <summary>
        /// Get data from an owner in a json file
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        //public async Task<bool> AllDataForOwnerAsync(int idOwner) //Task<List<Cave>>
        //{
        //    //var getAll = await context.Cave.Where(c => c.IdOwner == idOwner).ToListAsync();
        //    var getAllCaves = caveRepository.GetAllCaveFromAOwner(idOwner);
        //    IEnumerable<Cave> test = new (getAllCaves);
        //    //var getAllDrawers = drawerRepository.GetAllDrawerFromACave(idOwner);//idCave);
        //    //var getAllWines = wineRepository.GetAllWinesFromADrawerAsync(idOwner);

        //    string fileName = "C:\\Users\\toush\\OneDrive\\Bureau\\Cave\\wwwroot\\Resources\\Data.json";
        //    using FileStream createStream = File.Create(fileName);
        //    await JsonSerializer.SerializeAsync(createStream, (getAllCaves, getAllCaves));
        //    await createStream.DisposeAsync();

        //    return true;
        //}
        public async Task<List<Cave>> AllDataForOwner(int idOwner)
        {
            var getAll = await context.Cave.Where(c => c.OwnerId == idOwner).ToListAsync();
            return getAll;
        }
    }
}