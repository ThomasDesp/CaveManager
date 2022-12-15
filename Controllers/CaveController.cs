using CaveManager.Entities;
using CaveManager.Entities.DTO;
using CaveManager.Repository;
using CaveManager.Repository.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [NonAction]
        public bool IsConnected()
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Select Cave with his id
        /// </summary>
        /// <param name="idCave"></param>
        /// <returns></returns>
        [HttpGet("{idCave}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Cave>> GetCave(int idCave)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var cave = await caveRepository.SelectCaveAsync(idCave);
                if (cave != null)
                    return Ok(cave);
                else
                    return BadRequest("Cave not found");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Select All Owner's cave(s) with his id
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpGet("{idOwner}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<Cave>>> GetAllCaveFromOwner(int idOwner)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var caves = await caveRepository.GetAllCaveFromAOwner(idOwner);
                if (caves != null)
                {
                    if (caves.Count > 0)
                    {
                        return Ok(caves);
                    }
                    return BadRequest("This owner don't have any cave(s).");
                }
                else
                    return BadRequest("Owner not found");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Select all Cave's drawner(s) with his id
        /// </summary>
        /// <param name="idCave"></param>
        /// <returns></returns>
        [HttpGet("{idCave}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<Drawer>>> GetAllDrawer(int idCave)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var drawers = await caveRepository.GetAllDrawerFromACave(idCave);
                if (drawers != null)
                {
                    if (drawers.Count > 0)
                    {
                        return Ok(drawers);
                    }
                    return BadRequest("This cave don't have any drawer(s).");
                }
                return BadRequest("No Cave found");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Add a cave
        /// </summary>
        /// <param name="dtoCave"></param>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpPost("{idOwner}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Cave>> AddCave([FromForm]DTOCave dtoCave, int idOwner)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var cave = new Cave { Name = dtoCave.Name, OwnerId = idOwner };
                var caveCreated = await caveRepository.AddCaveAsync(cave);
                if (caveCreated != null)
                    return Ok(caveCreated);
                else
                    return BadRequest("Owner not found");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Modify a cave
        /// </summary> 
        /// <param name="idCave"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Cave>> UpdateCave(int id, string name)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var portedevoiture = await caveRepository.UpdateCaveAsync(id, name);

                if (portedevoiture != null)
                    return Ok(portedevoiture);
                else
                    return BadRequest("Cave not found");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Delete a Cave
        /// </summary>
        /// <param name="idCave></param>
        /// <returns></returns>
        [HttpDelete("{idCave}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Cave>> RemoveCave(int idCave)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var deleteCave = await caveRepository.RemoveCaveAsync(idCave);
                if (deleteCave != null)
                {
                    return Ok(deleteCave);
                }
                return BadRequest("Cave not found");
            }
            return BadRequest("Not logged");
        }
    }
}

