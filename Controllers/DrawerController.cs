using CaveManager.Entities;
using CaveManager.Entities.DTO;
using CaveManager.Repository;
using CaveManager.Repository.Repository.Contract;
using Microsoft.AspNetCore.Mvc;


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
        /// <summary>
        /// Select a drawer with his id
        /// </summary>
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        [HttpGet("{idDrawer}")]
        public async Task<ActionResult<Drawer>> GetDrawer(int idDrawer)
        {
            var drawers = await drawerRepository.SelectDrawerAsync(idDrawer);

            return Ok(drawers);
        }
        /// <summary>
        /// Get wines from a drawer
        /// </summary>
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        [HttpGet("{idDrawer}")]
        public async Task<ActionResult<List<Wine>>> GetAllWinesFromADrawer(int idDrawer)
        {
            var wines = await drawerRepository.GetAllWinesFromADrawerAsync(idDrawer);
            if (wines != null)
            {
                return Ok(wines);
            }
            return BadRequest("Drawers not found");

        }
        [HttpGet("{idDrawer}")]
        public async Task<ActionResult<DTODrawer>> AddDrawer(DTODrawer dTODrawer)
        {
            var nouveauDrawer = new Drawer { Name = dTODrawer.Name, MaxPlace = dTODrawer.MaxPlace, PlaceUsed = dTODrawer.PlaceUsed };
            var drawer = await drawerRepository.AddDrawerAsync(nouveauDrawer);
            if (drawer != null)
            {
                return Ok(dTODrawer);
            }
            return BadRequest();
        }
        /// <summary>
        /// Update a Drawer with his id, change all the data
        /// </summary>
        /// <param name="idDrawer"></param>
        /// <param name="dTODrawer"></param>
        /// <returns></returns>
        [HttpPut("{idDrawer}")]
        public async Task<ActionResult<Drawer>> UpdateDrawer(int idDrawer, DTODrawer dTODrawer)
        {
            var nouveauDrawer = new Drawer {Name=dTODrawer.Name, MaxPlace=dTODrawer.MaxPlace, PlaceUsed=dTODrawer.PlaceUsed };
            var drawer = await drawerRepository.UpdateDrawerAsync(idDrawer, nouveauDrawer);
            if (drawer != null)
            {
                return Ok(drawer);
            }
            return BadRequest("Drawer not found");
        }
        /// <summary>
        /// Delete a Drawer with his id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{idCave}")]
        public async Task<ActionResult<Drawer>> DeleteDrawer(int drawerId)
        {
            var drawer = await drawerRepository.RemoveDrawerAsync(drawerId);
            if (drawer != null)
            {
                return Ok(drawer);
            }
            return BadRequest("Drawer not found");
        }

    }
}
