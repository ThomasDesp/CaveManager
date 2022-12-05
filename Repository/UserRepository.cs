using CaveManager.Entities;
using CaveManager.Repository.Repository.Contract;

namespace CaveManager.Repository
{
    public class UserRepository : IUserRepository
    {
        //WikyContext context;
        //ILogger<UserRepository> logger;
        //public UserRepository(WikyContext context, ILogger<UserRepository> logger)
        //{
        //    this.context = context;
        //    this.logger = logger;
        //}

        /// <summary>
        /// Add an user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> AddUserAsync(User user)
        {
            var addUser = context.Wines.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Get an user by his id 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public async Task<Wine> SelectWineAsync(int idUser)
        {
            return await context.Users.FindAsync(idUser);
        }

        /// <summary>
        /// Update an user by his id
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<User> UpdateUserAsync(int idUser, string firstname, string lastname, string email, string password, int adress)
        {
            User userUpdate = await context.Users.FirstOrDefaultAsync(u => u.Id == idUser);
            userUpdate.FirstName = firstname;
            userUpdate.LastName = lastname;
            userUpdate.Fullname = firstname + lastname;
            userUpdate.Email = email;
            userUpdate.Password = password;
            userUpdate.Adress = adress;
            //userUpdate.PhoneNumbers =  ;

            await context.SaveChangesAsync();
            return userUpdate;
        }

        /// <summary>
        /// Remove wine from a Drawer with his id
        /// </summary>
        /// <param name="idWine"></param>
        /// <returns></returns>
        public async Task<bool> RemoveWineAsync(int idWine)
        {
            var deleteComment = await context.Wines.Where(w => w.Id == idWine).SingleOrDefaultAsync();
            context.Comments.Remove(deleteComment);
            context.SaveChanges();
            return true;
        }
    }
