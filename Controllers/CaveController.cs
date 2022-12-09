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


        [HttpGet]
        public async Task<ActionResult<Cave>> GetCave(int idCave)
        {
            var cave = await caveRepository.SelectCaveAsync(idCave);
            if (cave != null)
                return Ok(cave);
            else
                return BadRequest("Cave not found");
        }
        [HttpGet]
        public async Task<ActionResult<List<Cave>>> GetAllCaveFromOwner(int idOwner)
        {
            var caves = await caveRepository.GetAllCaveFromAOwner(idOwner);
            if (caves != null)
                return Ok(caves);
            else
                return BadRequest("Owner not found");
        }
        /// <summary>
        /// Select all Drawer of a Cave with his id
        /// </summary>
        /// <param name="idCave"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Drawer>>> GetAllDrawer(int idCave)
        {
            var drawers = await caveRepository.GetAllDrawerFromACave(idCave);
            if (drawers!= null)
            {
                return Ok(drawers);
            }
            return BadRequest("No Cave found");
            
        }

        [HttpPost]
        public async Task<ActionResult<Cave>> PostAddCave(Cave cave, int idOwner)
        {
            cave.OwnerId = idOwner;
            var caveCreated = await caveRepository.AddCaveAsync(cave);
            if (caveCreated != null)
                return Ok(caveCreated);
            else
                return BadRequest("Owner not found");
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Cave>> UpdateCave(int id, string name)
        {
            var portedevoiture = await caveRepository.UpdateCaveAsync(id, name);

            if (portedevoiture != null)
                return Ok(portedevoiture);
            else
                return BadRequest("Cave not found");

        }



        [HttpDelete("{idCave}")]
        public async Task<ActionResult<Cave>> RemoveCave(int idCave)
        {
            var deleteCave = await caveRepository.RemoveCaveAsync(idCave);
            if (deleteCave != null)
            {
                return Ok(deleteCave);

            }
            return BadRequest("Cave not found");

        }



    }
}

