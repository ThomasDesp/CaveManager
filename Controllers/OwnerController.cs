using CaveManager.Repository.Repository.Contract;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;
using CaveManager.Entities;
using CaveManager.Repository;

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

        /// <summary>
        /// Add Owner to Db
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Owner>> PostAddOwner(Owner owner)
        {
            var ownerCreated = await ownerRepository.AddOwnerAsync(owner);

            if (ownerCreated != null)
                return Ok(ownerCreated);
            else
                return BadRequest("Compte non créé, regardez les logs");
        }

        /// <summary>
        /// Get an owner by his id
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpGet("{idOwner}")]
        public async Task<ActionResult<Owner>> GetWine(int idOwner)
        {
            return Ok(await ownerRepository.SelectOwnerAsync(idOwner));
        }

        /// <summary>
        /// Modify an owner
        /// </summary>
        /// /// <param name="idOwner"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="email"></param>
        /// <param name="adress"></param>
        /// <param name="phoneNumber1"></param>
        /// <param name="phoneNumber2"></param>
        /// <param name="phoneNumber3"></param>
        /// <returns></returns>
        [HttpPut("{idOwner}")]
        public async Task<ActionResult<Owner>> PutOwner(int idOwner,string firstname, string lastname, string email, string adress, string phoneNumber1, string phoneNumber2, string phoneNumber3)
        {
            return Ok(await ownerRepository.UpdateOwnerAsync(idOwner,firstname, lastname, email, adress, phoneNumber1, phoneNumber2, phoneNumber3));
        }

        /// <summary>
        /// Delete an owner
        /// </summary>
        /// <param name="idOwner></param>
        /// <returns></returns>
        [HttpDelete("{idOwner}")]
        public async Task<ActionResult<bool>> DeleteOwner(int idOwner)
        {
            // Delete Wine, Drawner and Cave associated with idOwner
            await ownerRepository.DeleteCaveAsync(idOwner);
            await ownerRepository.DeleteOwnerAsync(idOwner);
            return Ok(true);
        }

        /// <summary>
        /// Method for login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> LoginAsync(
            [DefaultValue("moi@ho.com")] string email,
            [DefaultValue("toto")] string pwd)
        {
            var ownerCreated = await ownerRepository.RetrieveOwnerByPasswordAndLoginAsync(email,pwd);
            if (ownerCreated == null)
                return Problem($"Erreur");

            Claim emailClaim = new(ClaimTypes.Email, ownerCreated.Email);
            Claim nameClaim = new(ClaimTypes.Name, ownerCreated.LastName);
            Claim gvClaim = new(ClaimTypes.GivenName, ownerCreated.FirstName);
            Claim idClaim = new(ClaimTypes.NameIdentifier, ownerCreated.Id.ToString());

            ClaimsIdentity identity = new(new List<Claim> { emailClaim, nameClaim, gvClaim, idClaim }, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return Ok($"{ownerCreated.LastName}logged");
        }

        /// <summary>
        /// Method for logout
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok("Logout");
        }

        /// <summary>
        /// Delete Caves associated with an idOwner when deleting his account
        /// </summary> 
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpDelete("{idOwner}")]
        public async Task<ActionResult<bool>> DeleteCaves(int idOwner)
        {
            await ownerRepository.DeleteCaveAsync(idOwner);
            return Ok(true);
        }
    }
}
