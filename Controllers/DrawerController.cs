using CaveManager.Entities;
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

        [HttpGet]
        public async Task<ActionResult<Drawer>> GetDrawer(int idDrawer)
        {
            var drawers = await drawerRepository.SelectDrawerAsync(idDrawer);
            return Ok(drawers);
        }


        [HttpGet]
        public async Task<ActionResult<List<Drawer>>> GetAllDrawer(int idCave)
        {
            var drawers = await drawerRepository.GetAllDrawerFromACave(idCave);

            return Ok(drawers);
        }
        [HttpGet]
        public async Task<ActionResult<List<Wine>>> GetAllPeakWineFromOwner(int idOwner)
        {
            var wines = await drawerRepository.GetAllPeakWineFromOwnerAsync(idOwner);
            return Ok(wines);
        }
        [HttpPut]
        public async Task<ActionResult<Drawer>> UpdateDrawer(int Id, string Name, int MaxPlace, int PlaceUsed)
        {
            return Ok(drawerRepository.UpdateDrawerAsync(Id, Name, MaxPlace, PlaceUsed));
        }

        [HttpDelete]
        public async Task<ActionResult<Drawer>> DeleteDrawer(int Id)
        {
            return Ok(drawerRepository.RemoveDrawerAsync(Id));
        }
        [HttpGet("{idOwner}")]
        public async Task<ActionResult<List<Wine>>> GetAllWineFromOwner(int idOwner)
        {
            var wines = await drawerRepository.GetAllWineFromOwnerAsync(idOwner);
            return Ok(wines);
        }








    }
}
