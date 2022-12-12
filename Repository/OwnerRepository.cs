using CaveManager.Entities;
using CaveManager.Entities.DTO;
using CaveManager.Migrations;
using CaveManager.Repository.Repository.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace CaveManager.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        ICaveRepository caveRepository;
        IDrawerRepository drawerRepository;
        IWineRepository wineRepository;
        CaveManagerContext context;
        ILogger<OwnerRepository> logger;
        public OwnerRepository(CaveManagerContext context, ILogger<OwnerRepository> logger, ICaveRepository caveRepository, IDrawerRepository drawerRepository, IWineRepository wineRepository)
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
        public async Task<Owner> AddOwnerAsync(string firstname, string lastname, string password, string email, string address, string phoneNumber1, string phoneNumber2, string phoneNumber3)
        {
            var passwordValidated = Password.IsPasswordValidated(password);
            if (passwordValidated)
            {
                var passwordHashed = Password.HashPassword(password);
                Owner owner = new Owner { Id = 0, FirstName = firstname, LastName = lastname, Password = passwordHashed, Email = email, Address = address, PhoneNumber1 = phoneNumber1, PhoneNumber2 = phoneNumber2, PhoneNumber3 = phoneNumber3 };
                var addOwner = context.Owner.Add(owner);
                await context.SaveChangesAsync();
                return owner;
            }
            else
            {
                Owner owner = new Owner();
                return owner;
            }
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
        /// <param name="idOwner"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="phoneNumber1"></param>
        /// <param name="phoneNumber2"></param>
        /// <param name="phoneNumber3"></param>
        /// <returns></returns>
        public async Task<Owner> UpdateOwnerAsync(int idOwner, string firstname, string lastname, string email, string address, string phoneNumber1, string phoneNumber2, string phoneNumber3)
        {
            Owner ownerUpdate = await context.Owner.FirstOrDefaultAsync(o => o.Id == idOwner);
            ownerUpdate.FirstName = firstname;
            ownerUpdate.FullName = firstname + lastname;
            ownerUpdate.Email = email;
            ownerUpdate.Address = address;
            ownerUpdate.PhoneNumber1 = phoneNumber1;
            ownerUpdate.PhoneNumber2 = phoneNumber2;
            ownerUpdate.PhoneNumber3 = phoneNumber3;

            await context.SaveChangesAsync();
            return ownerUpdate;
        }

        /// <summary>
        /// Update owner's password
        /// </summary>
        /// <param name="idOwner"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<(string, bool)> UpdateOwnerPasswordAsync(int idOwner, string password)
        {
            Owner ownerUpdate = await context.Owner.FirstOrDefaultAsync(o => o.Id == idOwner);
            if (ownerUpdate == null)
                return ("Owner's Id dont exist", false);

            var passwordChecked = Password.IsPasswordValidated(password);
            if (passwordChecked)
            {
                Password.HashPassword(password);
                ownerUpdate.Password = password;
            }
            else
                return ("Password is incorrect please retry", false);

            await context.SaveChangesAsync();
            return ("Updated", true);
        }

        /// <summary>
        /// Remove an owner with his id
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        public async Task<Owner> DeleteOwnerAsync(int idOwner)
        {
            var deleteOwner = await context.Owner.Where(o => o.Id == idOwner).SingleOrDefaultAsync();
            context.Owner.Remove(deleteOwner);
            await context.SaveChangesAsync();
            return deleteOwner;
        }
        /// <summary>
        /// Looking for an owner with email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Owner> RetrieveOwnerByPasswordAndLoginAsync(string email, string password)
        {
            var t = Password.HashPassword(password);
            return await context.Owner.Where(o => o.Email == email && o.Password == t).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Delete cave by idOwner
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        public async Task<List<Cave>> DeleteCaveAsync(int idOwner)
        {
            var owner = await context.Cave.Where(c => c.OwnerId == idOwner).SingleOrDefaultAsync();
            var deleteCave = await RemoveAllCaves(idOwner);
            context.Cave.Remove(owner);
            await context.SaveChangesAsync();
            return deleteCave;
        }

        /// <summary>
        /// Remove all caves from an idOwner
        /// </summary>
        /// <param name="idCave"></param>
        /// <returns></returns>
        public async Task<List<Cave>> RemoveAllCaves(int idCave)
        {
            var deleteCave = await context.Cave.Where(c => c.Id == idCave).ToListAsync();
            foreach (var item in deleteCave)
            {
               await caveRepository.RemoveCaveAsync(item.Id);
            }
            return deleteCave;
        }

        /// <summary>
        /// Check if user have 18 years. If true => Creation of Owner's account, false => Account is not created
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
        public async Task<bool> CheckCgu(Owner owner)
        {
            if (owner.IsCGUAccepted)
                //Method to add his first cave
                return true;
            else
                return false;
        }

        /// <summary>
        /// Get data from an owner in a json file
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        public async Task<List<Wine>> AllDataForOwnerAsync(int idOwner) //Task<List<Cave>>
        {
            var winesList = await GetAllWineFromOwnerAsync(idOwner);
            try 
            {
                string fileName = $"wwwroot\\Ressources\\Data.json";
                using FileStream createStream = File.Create(fileName);
                await JsonSerializer.SerializeAsync(createStream, winesList,
                    new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.IgnoreCycles,
                        //Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Utf),
                        WriteIndented = true
                    });
                await createStream.DisposeAsync();
                return winesList;
            }
            catch (Exception e)
            {
                logger.LogError(e?.InnerException?.ToString());
                return winesList;
            }

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