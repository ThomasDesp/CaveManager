using CaveManager.Repository;
using CaveManager.Repository.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace CaveManager.Controllers
{
    [Route("[controller]/[action]")]
    [WineController]
    public class WineController : ControllerBase
    {
        IWineRepository wineRepository;
        public WineController(IWineRepository wineRepository)
        {
            this.wineRepository = wineRepository;
        }

    }
}
