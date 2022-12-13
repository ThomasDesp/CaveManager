using CaveManager.Repository.Repository.Contract;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;
using CaveManager.Entities;
using CaveManager.Repository;
using CaveManager.Entities.DTO;
using System.Text.Json;
using System;

namespace CaveManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        IOwnerRepository ownerRepository;
        IWebHostEnvironment environment;
        ILogger<OwnerRepository> logger;
        public OwnerController(IOwnerRepository ownerRepository, ILogger<OwnerRepository> logger, IWebHostEnvironment environment)
        {
            this.environment = environment;
            this.ownerRepository = ownerRepository;
            this.logger = logger;
        }

        [NonAction]
        public bool IsConnected()
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Add Owner to Database
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
                return BadRequest("Account is not created, please check the logs !");
        }

        /// <summary>
        /// Get an owner by his id
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpGet("{idOwner}")]
        public async Task<ActionResult<Owner>> GetOwner(int idOwner)
        {
            var getOwner = Ok(await ownerRepository.SelectOwnerAsync(idOwner));
            if (getOwner != null)
                return Ok(getOwner);
            else
                return BadRequest("Owner was not found !");
        }

        /// <summary>
        /// Modify an owner
        /// </summary>
        /// <param name="idOwner"></param>
        /// <param name="dTOOwnerModification"></param>
        /// <returns></returns>
        [HttpPut("{idOwner}")]
        public async Task<ActionResult<Owner>> PutOwner(int idOwner, [FromForm] DTOOwnerModification dTOOwnerModification)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var owner = new Owner
                {
                    FirstName = dTOOwnerModification.FirstName,
                    LastName = dTOOwnerModification.LastName,
                    Email = dTOOwnerModification.Email,
                    Address = dTOOwnerModification.Address,
                    PhoneNumber1 = dTOOwnerModification.PhoneNumber1,
                    PhoneNumber2 = dTOOwnerModification.PhoneNumber1,
                    PhoneNumber3 = dTOOwnerModification.PhoneNumber1
                };
                var putOwner = Ok(await ownerRepository.UpdateOwnerAsync(idOwner, owner));
                if (putOwner != null)
                    return Ok(putOwner);
                else
                    return BadRequest("Owner was not modified !");
            }
            return BadRequest("Not logged");
        }

        [HttpPut("{idOwner}")]
        public async Task<ActionResult<Owner>> PutOwnerPassword(int idOwner, string password)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var putOwnerPassword = await ownerRepository.UpdateOwnerPasswordAsync(idOwner, password);
                if (putOwnerPassword.Item2 == true)
                    return Ok(putOwnerPassword);
                else
                    return BadRequest("Owner was not modified !");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Delete an owner
        /// </summary>
        /// <param name="idOwner></param>
        /// <returns></returns>
        [HttpDelete("{idOwner}")]
        public async Task<ActionResult<Owner>> DeleteOwner(int idOwner)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                // Delete Wine, Drawner and Cave associated with idOwner

                var ownerDelete = await ownerRepository.DeleteOwnerAsync(idOwner);
                if (ownerDelete != null)
                    return Ok(ownerDelete);
                else
                    return BadRequest("Cave(s) and Owner was not deleted !");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Method for login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> LoginAsync(
            [DefaultValue("leo@gmail.com")] string email,
            [DefaultValue("1v9A")] string pwd)
        {
            var ownerCreated = await ownerRepository.RetrieveOwnerByPasswordAndLoginAsync(email, pwd);
            if (ownerCreated == null)
                return BadRequest($"Error");

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
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                await HttpContext.SignOutAsync();
                return Ok("Logout");
            }
            return BadRequest("Not logged");
        }



        /// <summary>
        /// Delete Caves associated with an idOwner when deleting his account
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpDelete("{idOwner}")]
        public async Task<ActionResult<List<Cave>>> DeleteCaves(int idOwner)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var caveDelete = await ownerRepository.RemoveAllCavesAsync(idOwner);
                if (caveDelete != null)
                    return Ok(caveDelete);
                else
                    return BadRequest("Cave(s) was not deleted !");
            }
            return BadRequest("Not logged");
        }
        /// <summary>
        /// Check age with user's birthday
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<bool>> CheckAge(DateTime birthDate)
        {
            var ageCheck = await ownerRepository.CheckAgeAsync(birthDate);
            if (ageCheck == true)
                return Ok(ageCheck);
            else
                return BadRequest("User is too young");
        }

        /// <summary>
        /// Save data as a json file
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpGet("{idOwner}")]
        public async Task<ActionResult<List<Wine>>> AllDataForOwner(int idOwner)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var jsonCreate = await ownerRepository.AllDataForOwnerAsync(idOwner);
                if (jsonCreate != null)
                    return Ok(jsonCreate);
                else
                    return BadRequest("Json file was not created, this owner do not exist");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Import data from a json file
        /// </summary>
        /// <param name="createStream"></param>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpPost("{idOwner}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<Wine>>> ImportDataForOwner(IFormFile createStream, int idOwner)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                List<Wine> wines = await ownerRepository.ImportDataForOwnerAsync(createStream.OpenReadStream(), idOwner);
                if (wines != null)
                    return Ok(wines);
                return BadRequest("Owner not found");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// List for all caves with theirs drawers and wines and only peak wine
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpGet("{idOwner}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<Wine>>> GetAllPeakWineFromOwner(int idOwner)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var wines = await ownerRepository.GetAllPeakWineFromOwnerAsync(idOwner);
                if (wines != null)
                    return Ok(wines);
                else
                    return BadRequest("The caves's list don't exist");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// List for all caves with theirs drawers and wines
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpGet("{idOwner}")]
        public async Task<ActionResult<List<Wine>>> GetAllWineFromOwner(int idOwner)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var wines = await ownerRepository.GetAllWineFromOwnerAsync(idOwner);
                if (wines != null)
                    return Ok(wines);
                else
                    return BadRequest("The cave's list don't exist");
            }
            return BadRequest("Not logged");
        }
    }
}