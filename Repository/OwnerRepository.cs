using CaveManager.Entities;
using CaveManager.Repository.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace CaveManager.Repository
{
    public class OwnerRepository : IOwner
    {
        CaveManagerContext context;
        ILogger<OwnerRepository> logger;
        public OwnerRepository(CaveManagerContext context, ILogger<OwnerRepository> logger)
        {
            this.context = context;
            this.logger = logger;
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
        public async Task<Owner> UpdateOwnerAsync(int idOwner, string firstname, string lastname, string email, string password, string adress)
        {
            Owner ownerUpdate = await context.Owner.FirstOrDefaultAsync(u => u.Id == idOwner);
            ownerUpdate.FirstName = firstname;
            ownerUpdate.LastName = lastname;
            ownerUpdate.FullName = firstname + lastname;
            ownerUpdate.Email = email;
            ownerUpdate.Password = password;
            ownerUpdate.Adress = adress;
            //ownerUpdate.PhoneNumbers =  ;

            await context.SaveChangesAsync();
            return ownerUpdate;
        }

        /// <summary>
        /// Remove an owner with his id
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        public async Task<bool> RemoveOwnerAsync(int idOwner)
        {
            var deleteOwner = await context.Owner.Where(w => w.Id == idOwner).SingleOrDefaultAsync();
            context.Owner.Remove(deleteOwner);
            context.SaveChanges();
            return true;
        }
    }
}