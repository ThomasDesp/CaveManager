using CaveManager.Repository.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace CaveManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        IOwner ownerRepository;
        public OwnerController(IOwner ownerRepository)
        {
            this.ownerRepository = ownerRepository;
        }


    }
}
