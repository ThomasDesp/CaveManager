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
        public async Task<ActionResult<Cave>> PostAddCave(Cave cave,int idUser)
        {
            cave.IdUser = idUser;
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
                    IdUser = cave.IdUser,

                };

                new Cave
                {
                    Id = 2,
                    Name = "PouCave",
                    IdUser = cave.IdUser,

                };
            }
            return Ok(cave);
        }
        [HttpPut("{id}")]
        public async ActionResult<Cave> UpdateCave( int id,string name ) 
        {
            if (cave.Id != id)
                return BadRequest();
            var portedevoiture = await caveRepository.UpdateCaveAsync(id , name);













        }
        [HttpDelete("{id}")]
        public ActionResult<Cave> DeleteCave([FromRoute] int id, [FromBody] Cave cave) 
        {
            if (cave.Id != id )
                return BadRequest();

                return Ok(cave);
        
        }

    }
}

