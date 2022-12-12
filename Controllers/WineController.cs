using CaveManager.Entities;
using CaveManager.Entities.DTO;
using CaveManager.Repository;
using CaveManager.Repository.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace CaveManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class WineController : ControllerBase
    {
        IWineRepository wineRepository;
        private readonly ILogger<WineController> _logger;
        public WineController(IWineRepository wineRepository, ILogger<WineController> logger)
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
        [HttpPost("{idDrawer}")]
        public async Task<ActionResult<Wine>> AddWine(Wine wine, int idDrawer)
        {
            var wineAdded = await wineRepository.AddWineAsync(wine, idDrawer);

            if (wineAdded.error != "ok")
                return Ok(wineAdded.wine);
            else
                return BadRequest(wineAdded.error);
        }


        /// <summary>
        /// Get a wine by his id
        /// </summary>
        /// <param name="idWine"></param>
        /// <returns></returns>
        [HttpGet("{idWine}")]
        public async Task<ActionResult<Wine>> GetWine(int idWine)
        {
            var wine = await wineRepository.GetWineAsync(idWine);
            if (wine != null) return Ok(wine);
            return BadRequest("Wine not found");
        }

        /// <summary>
        /// Delete a wine
        /// </summary>
        /// <param name="idWine></param>
        /// <returns></returns>
        [HttpDelete("{idWine}")]
        public async Task<ActionResult<Wine>> DeleteWine(int idWine)
        {
            var wine = await wineRepository.DeleteWineAsync(idWine);
            if(wine != null) return Ok(wine);
            return BadRequest("Wine not found, try with a correct id");
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
        public async Task<ActionResult<Wine>> PutWine(int idWine, DTOWine dTOWine)
        {
            var nouveauDrawer = new Wine { Name = dTOWine.Name, Type = dTOWine.Type, Designation = dTOWine.Designation , Bottling=dTOWine.Bottling,MaxVintageRecommended=dTOWine.MaxVintageRecommended, MinVintageRecommended=dTOWine.MinVintageRecommended};
            var wine = await wineRepository.PutWineAsync(nouveauDrawer, idWine);
            if (wine != null)
            {
                return Ok(wine);
            }
            return BadRequest("Wine not found, try with a correct id");
            
        }

        /// <summary>
        /// Duplicate a wine and add it to a specific drawer
        /// </summary>
        /// <param name="idWine"></param>
        /// <param name="idDrawer"></param>
        /// <returns></returns>
        [HttpPost("{idDrawer}")]
        public async Task<ActionResult<Wine>> DuplicateWine(int idWine, int idDrawer)
        {
            var wine = await wineRepository.DuplicateWineAsync(idWine, idDrawer);
            if (wine.error != "ok")
                return Ok(wine.wine);
            else
                return BadRequest(wine.error);
        }
    }
}
