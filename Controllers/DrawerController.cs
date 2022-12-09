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
        IDrawer drawerRepository;
        IWebHostEnvironment environment;
        private readonly ILogger<DrawerController> _logger;
        public DrawerController(IDrawer drawerRepository, ILogger<DrawerController> logger, IWebHostEnvironment environment)
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
        [HttpGet]
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
            return Ok(await drawerRepository.GetAllWinesFromADrawerAsync(idDrawer));
        }
        
        /// <summary>
        /// Update a Drawer with his id, change all the data
        /// </summary>
        /// <param name="idDrawer"></param>
        /// <param name="dTODrawer"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Drawer>> UpdateDrawer(int idDrawer, DTODrawer dTODrawer)
        {
            var drawer = await drawerRepository.UpdateDrawerAsync(idDrawer,dTODrawer);
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
        [HttpDelete]
        public async Task<ActionResult<Drawer>> DeleteDrawer(int Id)
        {
            return Ok(drawerRepository.RemoveDrawerAsync(Id));
        }

    }
}
