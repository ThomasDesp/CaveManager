using CaveManager.Entities;
using CaveManager.Repository;
using CaveManager.Repository.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace CaveManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class WineController : ControllerBase
    {
        IWine wineRepository;
        private readonly ILogger<WineController> _logger;
        public WineController(IWine wineRepository, ILogger<WineController> logger)
        {
            this.wineRepository = wineRepository;
            _logger = logger;
        }

        /// <summary>
        /// Add a Wine to Drawer
        /// </summary>
        /// <param name="wine"></param>
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Wine>> AddWine(Wine wine, int idDrawer)
        {
            var wineAdded = await wineRepository.AddWineAsync(wine, idDrawer);

            if (wineAdded != null)
                return Ok(wineAdded);
            else
                return BadRequest("Wine is not added, please retry");
        }

        /// <summary>
        /// Get wines from a drawer
        /// </summary>
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        [HttpGet("{idDrawer}")]
        public async Task<ActionResult<List<Wine>>> GetAllWinesFromADrawer(int idDrawer)
        {
            return Ok(await wineRepository.GetAllWinesFromADrawerAsync(idDrawer));
        }

        /// <summary>
        /// Get a wine by his id
        /// </summary>
        /// <param name="idWine"></param>
        /// <returns></returns>
        [HttpGet("{idWine}")]
        public async Task<ActionResult<Wine>> GetWine(int idWine)
        {
            return Ok(await wineRepository.GetWineAsync(idWine));
        }

        /// <summary>
        /// Delete a wine
        /// </summary>
        /// <param name="idWine></param>
        /// <returns></returns>
        [HttpDelete("{idWine}")]
        public async Task<ActionResult<bool>> DeleteWine(int idWine)
        {
            
            await wineRepository.DeleteWineAsync(idWine);
            return Ok(true);
        }

        /// <summary>
        /// Modify a wine
        /// </summary> 
        /// <param name="idWine"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="designation"></param>
        /// <param name="minVintageRecommended"></param>
        /// <param name="maxVintageRecommended"></param>
        /// <returns></returns>
        [HttpPut("{idWine}")]
        public async Task<ActionResult<Wine>> PutWine(int idWine, string name, string type, string designation, int minVintageRecommended, int maxVintageRecommended)
        {
            return Ok(await wineRepository.PutWineAsync(idWine,name,type,designation,minVintageRecommended, maxVintageRecommended));
        }
    }
}
