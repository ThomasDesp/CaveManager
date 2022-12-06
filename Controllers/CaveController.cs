using CaveManager.Repository;
using CaveManager.Repository.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace CaveManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class CaveController : ControllerBase
    {
        ICave caveRepository;
        IWebHostEnvironment environment;
        private readonly ILogger<CaveController> _logger;
        public CaveController(ICave caveRepository, ILogger<CaveController> logger, IWebHostEnvironment environment)
        {
            this.caveRepository = caveRepository;
            _logger = logger;
        }


    }
}

