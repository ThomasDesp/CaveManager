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
        public WineController(IWine wineRepository)
        {
            this.wineRepository = wineRepository;
        }


    }
}
