using CaveManager.Entities;
using CaveManager.Entities.DTO;
using CaveManager.Repository;
using CaveManager.Repository.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace CaveManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class CaveController : ControllerBase
    {
        ICaveRepository caveRepository;
        IWebHostEnvironment environment;
        private readonly ILogger<CaveController> _logger;
        public CaveController(ICaveRepository caveRepository, ILogger<CaveController> logger, IWebHostEnvironment environment)
        {
            this.caveRepository = caveRepository;
            _logger = logger;
        }

        /// <summary>
        /// select Cave with his id
        /// </summary>
        /// <param name="idCave"></param>
        /// <returns></returns>
        [HttpGet("{idCave}")]
        public async Task<ActionResult<Cave>> GetCave(int idCave)
        {
            var cave = await caveRepository.SelectCaveAsync(idCave);
            if (cave != null)
                return Ok(cave);
            else
                return BadRequest("Cave not found");
        }
        /// <summary>
        /// Select All Cave of a Owner with his id
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpGet("{idOwner}")]
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
        [HttpGet("{idCave}")]
        public async Task<ActionResult<List<Drawer>>> GetAllDrawer(int idCave)
        {
            var drawers = await caveRepository.GetAllDrawerFromACave(idCave);
            if (drawers!= null)
            {
                return Ok(drawers);
            }
            return BadRequest("No Cave found");
            
        }

        /// <summary>
        /// Add a cave
        /// </summary>
        /// <param name="cave"></param>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpPost("{idOwner}")]
        public async Task<ActionResult<Cave>> AddCave(DTOCave dtoCave, int idOwner)
        {
            var cave = new Cave { Name = dtoCave.Name, OwnerId = idOwner};
            var caveCreated = await caveRepository.AddCaveAsync(cave);
            if (caveCreated != null)
                return Ok(caveCreated);
            else
                return BadRequest("Owner not found");
        }

        /// <summary>
        /// Modify a cave
        /// </summary> 
        /// <param name="idCave"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Cave>> UpdateCave(int id, string name)
        {
            var portedevoiture = await caveRepository.UpdateCaveAsync(id, name);

            if (portedevoiture != null)
                return Ok(portedevoiture);
            else
                return BadRequest("Cave not found");

        }


        /// <summary>
        /// Delete a Cave
        /// </summary>
        /// <param name="idCave></param>
        /// <returns></returns>
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

