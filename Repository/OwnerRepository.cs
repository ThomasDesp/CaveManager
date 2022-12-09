using CaveManager.Entities;
using CaveManager.Entities.DTO;
using CaveManager.Repository.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

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
        //public async Task<Owner> AddOwnerAsync(DTOOwnerCreation dTOOwnerCreation, string password)
        //{
        //    password = Password;
        //    var passwordValidated = Password.IsPasswordValidated(password);
        //    if (passwordValidated)
        //    {
        //        Password.HashPassword(password);
        //        var addOwner = context.Owner.Add(dTOOwnerCreation,password);
        //        await context.SaveChangesAsync();
        //    }
        //    return owner;


        //}

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
            ownerUpdate.Address = adress;
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
            var passwordChecked = Password.IsPasswordValidated(password);
            if (passwordChecked)
                ownerUpdate.Password = password;
            else
                return new Tuple<string, bool>("Password is incorrect please retry", false);

            await context.SaveChangesAsync();
            return new Tuple<string, bool>("Updated", true);
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
        /// Check if user have 18 years. If true => Creation of Owner's account, false => Accont is not created
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        public async Task<bool> CheckAgeAsync(DateTime birthDate)
        {
            var ageSub = DateTime.Now - birthDate;
            //6570 days is the number of days for 18 years
            if (ageSub.TotalDays >= 6570)
            {
                //Appel création compte (dans créa compte appel check cgu)

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Check if CGU are accepted or not. If Owner don't accept the CGU, he can't continue
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public async Task<bool> CheckCguAsync(Owner owner)
        {
            if (owner.IsCGUAccepted)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Get data from an owner in a json file
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        public async Task<bool> AllDataForOwnerAsync(int idOwner) //Task<List<Cave>>
        {
            var getAllCaves = GetAllWineFromOwnerAsync(idOwner);
            string fileName = "C:\\Users\\toush\\OneDrive\\Bureau\\Cave\\wwwroot\\Resources\\Data.json";
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, getAllCaves,
                new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    //Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Utf),
                    WriteIndented = true
                });
            await createStream.DisposeAsync();
            return true;
        }

        //public async Task<List<Cave>> AllDataForOwner(int idOwner)
        //{
        //    var getAll = await context.Cave.Where(c => c.OwnerId == idOwner).ToListAsync();
        //    return getAll;
        //}

        /// <summary>
        /// List for all caves with theirs drawers and wines
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        public async Task<List<Wine>> GetAllWineFromOwnerAsync(int idOwner)
        {
            //var wines = await context.Wine.Include(w => w.Drawer).ThenInclude(d => d.Cave).Where(w => w.Drawer.IdCave == idCave && w.Bottling+w.MinVintageRecommended <= date && w.Bottling + w.MaxVintageRecommended >= date ).ToListAsync();
            var wines = await context.Wine.Include(w => w.Drawer).ThenInclude(d => d.Cave).ThenInclude(o => o.Owner)
                .Where(w => w.Drawer.Cave.OwnerId == idOwner).ToListAsync();
            return wines;
        }

        /// <summary>
        /// List for all caves with theirs drawers and wines and only peak wine
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        public async Task<List<Wine>> GetAllPeakWineFromOwnerAsync(int idOwner)
        {
            int date = DateTime.Now.Year;
            var wines = await context.Wine.Include(w => w.Drawer).ThenInclude(d => d.Cave).ThenInclude(o => o.Owner)
                .Where(w => w.Drawer.Cave.OwnerId == idOwner && w.Bottling + w.MinVintageRecommended <= date && w.Bottling
                + w.MaxVintageRecommended >= date).ToListAsync();
            return wines;
        }
    }
}