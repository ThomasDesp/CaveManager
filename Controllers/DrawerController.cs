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
    public class DrawerController : ControllerBase
    {
        IDrawerRepository drawerRepository;
        IWebHostEnvironment environment;
        private readonly ILogger<DrawerController> _logger;
        public DrawerController(IDrawerRepository drawerRepository, ILogger<DrawerController> logger, IWebHostEnvironment environment)
        {
            this.drawerRepository = drawerRepository;
            this.environment = environment;
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
        /// Select a drawer with his id
        /// </summary>
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        [HttpGet("{idDrawer}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Drawer>> GetDrawer(int idDrawer)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var drawer = await drawerRepository.SelectDrawerAsync(idDrawer);
                if (drawer != null)
                    return Ok(drawer);
                return BadRequest("This drawer was not found");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Get wines from a drawer
        /// </summary>
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        [HttpGet("{idDrawer}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<Wine>>> GetAllWinesFromADrawer(int idDrawer)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var wines = await drawerRepository.GetAllWinesFromADrawerAsync(idDrawer);

                if (wines != null)
                {
                    if (wines.Count > 0)
                        return Ok(wines);
                    return BadRequest("This drawer don't have any wine(s).");
                }
                return BadRequest("Drawers not found");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Add a drawer
        /// </summary>
        /// <param name="idCave"></param>
        /// <param name="dTODrawer"></param>
        /// <returns></returns>
        [HttpGet("{idCave}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<DTODrawer>> AddDrawer(int idCave,[FromForm]DTODrawer dTODrawer)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var newDrawer = new Drawer { Name = dTODrawer.Name, MaxPlace = dTODrawer.MaxPlace, PlaceUsed = 0 };
                var drawer = await drawerRepository.AddDrawerAsync(newDrawer, idCave);
                if (drawer != null)
                {
                    return Ok(dTODrawer);
                }
                return BadRequest();
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Update a Drawer with his id, change all the data
        /// </summary>
        /// <param name="idDrawer"></param>
        /// <param name="dTODrawer"></param>
        /// <returns></returns>
        [HttpPut("{idDrawer}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<(Drawer drawer, string error)>> UpdateDrawer(int idDrawer, [FromForm] DTODrawer dTODrawer)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var newDrawer = new Drawer { Name = dTODrawer.Name, MaxPlace = dTODrawer.MaxPlace};
                var drawer = await drawerRepository.UpdateDrawerAsync(idDrawer, newDrawer);
                if (drawer.error == "ok")
                {
                    return Ok(drawer.drawer);
                }
                return BadRequest(drawer.error);
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Delete a Drawer with his id
        /// </summary>
        /// <param name="drawerId"></param>
        /// <returns></returns>
        [HttpDelete("{drawerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Drawer>> DeleteDrawer(int drawerId)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var drawer = await drawerRepository.RemoveDrawerAsync(drawerId);
                if (drawer != null)
                {
                    return Ok(drawer);
                }
                return BadRequest("Drawer not found");
            }
            return BadRequest("Not logged");
        }
    }
}
