using CaveManager.Entities;
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

        
        [HttpPost]
        public async Task<ActionResult<Cave>> PostAddCave(Cave cave,int idOwner)
        {
            cave.OwnerId = idOwner;
            var caveCreated = await caveRepository.AddCaveAsync(cave);

            if (caveCreated != null)
                return Ok(caveCreated);
            else
                return BadRequest("Cave non crée");
        }

        [HttpGet]
        public async Task<ActionResult<Cave>> GetAddCave(Cave cave)
        {
            var caves = new List<Cave>();
            {
                new Cave
                {
                    Id = 1,
                    Name = "BatCave",
                    OwnerId = cave.OwnerId,

                };

                new Cave
                {
                    Id = 2,
                    Name = "PouCave",
                    OwnerId = cave.OwnerId,

                };
            }
            return Ok(cave);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Cave>> UpdateCave( int id,string name ) 
        { 
            var portedevoiture = await caveRepository.UpdateCaveAsync(id , name);
            return Ok(portedevoiture);

        }
       
        
        
        [HttpDelete("{idCave}")]
        public async Task<ActionResult<bool>> RemoveCave(int idCave)
        {
            await caveRepository.RemoveCaveAsync(idCave);
           
                
            return Ok(true);
        }

    }
}

