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
using CaveManager.Migrations;

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
                return false;
            else
                return true;
        }

        /// <summary>
        /// Get an owner by his id
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpGet("{idOwner}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Owner>> GetOwner(int idOwner)
        {
            var getOwner = await ownerRepository.SelectOwnerAsync(idOwner);
            if (getOwner != null)
                return Ok(getOwner);
            else
                return BadRequest("Owner was not found !");
        }

        /// <summary>
        /// Method for login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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
            return Ok($"{ownerCreated.LastName} logged");
        }

        /// <summary>
        /// Method for logout
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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
        /// Save data as a json file
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpGet("{idOwner}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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
                {
                    if (wines.Count > 0)
                        return Ok(wines);
                    return BadRequest("No wine found");
                }
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<Wine>>> GetAllWineFromOwner(int idOwner)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var wines = await ownerRepository.GetAllWineFromOwnerAsync(idOwner);
                if (wines != null)
                {
                    if (wines.Count > 0)
                        return Ok(wines);
                    return BadRequest("No wine found");
                }
                else
                    return BadRequest("The cave's list don't exist");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Add Owner to Database
        /// </summary>
        /// <param name="dTOOwner"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Owner>> PostAddOwner([FromForm] DTOOwner dTOOwner)
        {
            if (dTOOwner.Password == null) 
            {
                return BadRequest("Password is required !");
            }
            var owner = new Owner
            {
                FirstName = dTOOwner.FirstName,
                LastName = dTOOwner.LastName,
                FullName = dTOOwner.FullName,
                Email = dTOOwner.Email,
                Password = dTOOwner.Password,
                Address = dTOOwner.Address,
                PhoneNumber1 = dTOOwner.PhoneNumber1,
                PhoneNumber2 = dTOOwner.PhoneNumber2,
                PhoneNumber3 = dTOOwner.PhoneNumber3
            };
            var ownerCreated = await ownerRepository.AddOwnerAsync(owner);

            if (ownerCreated.error == "ok")
                return Ok(ownerCreated.owner);
            else
                return BadRequest($"Account is not created, please check the logs ! {ownerCreated.error}");
        }

        /// <summary>
        /// Check age with owner's birthday
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<bool>> CheckAge(DateTime birthDate)
        {
            var ageCheck = await ownerRepository.CheckAgeAsync(birthDate);
            if (ageCheck == true)
                return Ok(ageCheck);
            else
                return BadRequest("User is too young");
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
        /// Modify an owner
        /// </summary>
        /// <param name="idOwner"></param>
        /// <param name="dTOOwnerModification"></param>
        /// <returns></returns>
        [HttpPut("{idOwner}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Owner>> PutOwner(int idOwner, [FromForm] DTOOwnerModification dTOOwnerModification)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                if (dTOOwnerModification.FirstName == null || dTOOwnerModification.LastName == null || dTOOwnerModification.Email == null)
                {
                    return BadRequest("Firstname, lastname and email are required");
                }
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
                var putOwner = await ownerRepository.UpdateOwnerAsync(idOwner, owner);
                if (putOwner != null)
                    return Ok(putOwner);
                else
                    return BadRequest("Owner was not found");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Update owner's password
        /// </summary>
        /// <param name="idOwner"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPut("{idOwner}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<string>> PutOwnerPassword(int idOwner, string password)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                if (password == null)
                {
                    return BadRequest("Password is required");
                }
                var putOwnerPassword = await ownerRepository.UpdateOwnerPasswordAsync(idOwner, password);
                if (putOwnerPassword.Item2 == true)
                    return Ok("Password was modified");
                else
                    return BadRequest($"Password was not modified ! {putOwnerPassword.Item1}");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Delete an owner
        /// </summary>
        /// <param name="idOwner></param>
        /// <returns></returns>
        [HttpDelete("{idOwner}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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
                    return BadRequest("Owner don't found");
            }
            return BadRequest("Not logged");
        }

        /// <summary>
        /// Delete Caves associated with an idOwner when deleting his account
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpDelete("{idOwner}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<Cave>>> DeleteCaves(int idOwner)
        {
            bool checkIsConnected = IsConnected();
            if (checkIsConnected)
            {
                var caveDelete = await ownerRepository.RemoveAllCavesAsync(idOwner);
                if (caveDelete != null)
                {
                    if (caveDelete.Count > 0)
                        return Ok(caveDelete);
                    return BadRequest("This owner don't have any cave(s)");
                }
                else
                    return BadRequest("Cave(s) was not deleted !");
            }
            return BadRequest("Not logged");
        }
    }
}