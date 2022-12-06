using CaveManager.Repository.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace CaveManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class CaveController
    {
        ICave caveRepository;
        public CaveController(ICave caveRepository)
        {
            this.caveRepository = caveRepository;
        }
    }
}

