using Microsoft.VisualStudio.TestTools.UnitTesting;
using CaveManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CaveManager.Entities;
using CaveManager.Entities.DTO;
using CaveManager.Repository;
using CaveManager.Repository.Repository.Contract;

namespace CaveManager.Repository.Tests
{
    [TestClass()]
    public class OwnerRepositoryTests
    {

        [TestMethod()]
        public async Task CheckAgeAsyncTestSuccess()
        {
            var date = new DateTime(1999, 10, 05);


            OwnerRepository _ownerRepository = new OwnerRepository(null, null, null, null, null);
            //CaveManagerContext context, ILogger<OwnerRepository> logger, ICaveRepository caveRepository, IDrawerRepository drawerRepository, IWineRepository wineRepository
            var res = await _ownerRepository.CheckAgeAsync(date);

            Assert.AreEqual(res, true);
        }

        [TestMethod()]
        public async Task CheckAgeAsyncTestFailTooYoung()
        {
            var date = new DateTime(2018, 10, 05);


            OwnerRepository _ownerRepository = new OwnerRepository(null, null, null, null, null);
            //CaveManagerContext context, ILogger<OwnerRepository> logger, ICaveRepository caveRepository, IDrawerRepository drawerRepository, IWineRepository wineRepository
            var res = await _ownerRepository.CheckAgeAsync(date);

            Assert.AreEqual(res, false);
        }

        [TestMethod()]
        public async Task CheckAgeAsyncTestFailDateFromTheFuture()
        {
            var date = new DateTime(2100, 10, 05);


            OwnerRepository _ownerRepository = new OwnerRepository(null, null, null, null, null);
            //CaveManagerContext context, ILogger<OwnerRepository> logger, ICaveRepository caveRepository, IDrawerRepository drawerRepository, IWineRepository wineRepository
            var res = await _ownerRepository.CheckAgeAsync(date);

            Assert.AreEqual(res, false);
        }


        [TestMethod()]
        public async Task GetAllUsersAsyncTestSuccess()
        {
            // Bdd create
            var builder = new DbContextOptionsBuilder<CaveManagerContext>().UseInMemoryDatabase("CaveManagerTest");
            // Context
            var context = new CaveManagerContext(builder.Options);
            OwnerRepository userRepository = new OwnerRepository(context, null, null, null, null);
            var owner = new Owner
            {
                FirstName = "toto",
                LastName = "des",
                Email = "thom@gmail.com",
                Address = "rue",
                PhoneNumber1 = "0651151515",
                Password = "Z256D89A!t"
            };
            // Process GetAllUsersAsync //
            var res = await userRepository.AddOwnerAsync(owner);

            // Test
            Assert.AreEqual(1, context.Owner.Count());

            // Bdd delete
            context.Database.EnsureDeleted();
        }
    }
}