using CaveManager.Entities;
using CaveManager.Entities.DTO;
using CaveManager.Migrations;
using CaveManager.Repository.Repository.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
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
        public async Task<Owner> AddOwnerAsync(Owner owner)
        {
            var password = owner.Password;
            var passwordValidated = Password.IsPasswordValidated(password);
            if (passwordValidated)
            {
                var firstName = owner.FirstName;
                var lastName = owner.LastName;
                var email = owner.Email;
                var address = owner.Address;
                var phoneNumber1 = owner.PhoneNumber1;
                var phoneNumber2 = owner.PhoneNumber2;
                var phoneNumber3 = owner.PhoneNumber3;
                var passwordHashed = Password.HashPassword(password);
                Owner ownerAdded = new Owner { FirstName = firstName, LastName = lastName, Password = passwordHashed, Email = email, Address = address, PhoneNumber1 = phoneNumber1, PhoneNumber2 = phoneNumber2, PhoneNumber3 = phoneNumber3 };
                var addOwner = context.Owner.Add(ownerAdded);
                await context.SaveChangesAsync();
                return ownerAdded;
            }
            else
            {
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
        public async Task<Owner> UpdateOwnerAsync(int idOwner, Owner owner)
        {
            Owner ownerUpdate = await context.Owner.FirstOrDefaultAsync(o => o.Id == idOwner);
            ownerUpdate.FirstName = owner.FirstName;
            ownerUpdate.Email = owner.Email;
            ownerUpdate.Address = owner.Address;
            ownerUpdate.PhoneNumber1 = owner.PhoneNumber1;
            ownerUpdate.PhoneNumber2 = owner.PhoneNumber2;
            ownerUpdate.PhoneNumber3 = owner.PhoneNumber3;

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
                var passwordHashed = Password.HashPassword(password);
                ownerUpdate.Password = passwordHashed;
            }
            else
                return ("Password is incorrect please retry", false);

            await context.SaveChangesAsync();
            return ("Updated", true);
        }

        /// <summary>
        /// Looking for an owner with email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Owner> RetrieveOwnerByPasswordAndLoginAsync(string email, string password)
        {
            var passwordDb = Password.HashPassword(password);
            return await context.Owner.Where(o => o.Email == email && o.Password == passwordDb).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Delete cave by idOwner
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        public async Task<Owner> DeleteOwnerAsync(int idOwner)
        {
            var owner = await context.Owner.Where(c => c.Id == idOwner).SingleOrDefaultAsync();
            var deleteCave = await RemoveAllCavesAsync(idOwner);
            context.Owner.Remove(owner);
            await context.SaveChangesAsync();
            return owner;
        }

        /// <summary>
        /// Remove all caves from an idOwner
        /// </summary>
        /// <param name="idCave"></param>
        /// <returns></returns>
        public async Task<List<Cave>> RemoveAllCavesAsync(int ownerId)
        {
            var deleteCave = await context.Cave.Where(c => c.OwnerId == ownerId).ToListAsync();
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
        public async Task<List<Wine>> AllDataForOwnerAsync(int idOwner)
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
        public async Task<List<Wine>> ImportDataForOwnerAsync(Stream createStream, int idOwner)
        {
            List<Wine> wines = new List<Wine>();
            using (createStream/*var fileStreamRead = new FileStream("garageVoiture.xml", FileMode.Open, FileAccess.Read)*/)
            {
                StreamReader reader = new StreamReader(createStream);
                var serializedList = reader.ReadToEnd();
                var deserializedList = JsonSerializer.Deserialize<List<Wine>>(serializedList);

                foreach (var item in deserializedList)
                {
                    item.Drawer.Cave.OwnerId=idOwner;
                    item.Id = 0;
                    item.Drawer.Id = 0;
                    item.DrawerId = 0;
                    item.Drawer.Cave.Id = 0;
                    item.Drawer.CaveId = 0;
                    item.Drawer.Cave.Owner=null;
                    item.Drawer.Wines.Clear();
                    item.Drawer.Cave.Drawer.Clear();
                }
                context.Wine.AddRange(deserializedList);
                await context.SaveChangesAsync();
                return deserializedList;
            }
            return wines;

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