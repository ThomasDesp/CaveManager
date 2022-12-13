﻿using CaveManager.Repository.Repository.Contract;
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
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="phoneNumber1"></param>
        /// <param name="phoneNumber2"></param>
        /// <param name="phoneNumber3"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Owner>> PostAddOwner(string firstname, string lastname, string password, string email, string address, string phoneNumber1, string phoneNumber2, string phoneNumber3)
        {
            var ownerCreated = await ownerRepository.AddOwnerAsync(firstname, lastname, password, email, address, phoneNumber1, phoneNumber2, phoneNumber3);

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
        public async Task<ActionResult<Owner>> GetWine(int idOwner)
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
        /// /// <param name="idOwner"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="phoneNumber1"></param>
        /// <param name="phoneNumber2"></param>
        /// <param name="phoneNumber3"></param>
        /// <returns></returns>
        [HttpPut("{idOwner}")]
        public async Task<ActionResult<Owner>> PutOwner(int idOwner, string firstname, string lastname, string email, string address, string phoneNumber1, string phoneNumber2, string phoneNumber3)
        {
            var putOwner = Ok(await ownerRepository.UpdateOwnerAsync(idOwner, firstname, lastname, email, address, phoneNumber1, phoneNumber2, phoneNumber3));
            if (putOwner != null)
                return Ok(putOwner);
            else
                return BadRequest("Owner was not modified !");
        }

        [HttpPut("{idOwner}")]
        public async Task<ActionResult<Owner>> PutOwnerPassword(int idOwner, string password)
        {
            var putOwnerPassword = Ok(await ownerRepository.UpdateOwnerPasswordAsync(idOwner, password));
            if (putOwnerPassword != null)
                return Ok(putOwnerPassword);
            else
                return BadRequest("Owner was not modified !");
        }

        /// <summary>
        /// Delete an owner
        /// </summary>
        /// <param name="idOwner></param>
        /// <returns></returns>
        [HttpDelete("{idOwner}")]
        public async Task<ActionResult<Owner>> DeleteOwner(int idOwner)
        {
            // Delete Wine, Drawner and Cave associated with idOwner
           
            var ownerDelete = await ownerRepository.DeleteOwnerAsync(idOwner);
            if ( ownerDelete != null)
                return Ok(ownerDelete);
            else
                return BadRequest("Cave(s) and Owner was not deleted !");
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
            await HttpContext.SignOutAsync();
            return Ok("Logout");
        }

       

        /// <summary>
        /// Delete Caves associated with an idOwner when deleting his account
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpDelete("{idOwner}")]
        public async Task<ActionResult<List<Cave>>> DeleteCaves(int idOwner)
        {
            var caveDelete = await ownerRepository.RemoveAllCavesAsync(idOwner);
            if (caveDelete != null)
                return Ok(caveDelete);
            else
                return BadRequest("Cave(s) was not deleted !");
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
            var jsonCreate = await ownerRepository.AllDataForOwnerAsync(idOwner);
            if (jsonCreate != null)
                return Ok(jsonCreate);
            else
                return BadRequest("Json file was not created, this owner do not exist");
        }

        [HttpPost("{idOwner}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<Wine>>> ImportDataForOwner(IFormFile createStream, int idOwner/*[FromForm] string Jfile*/)
        {
            List<Wine> wines = await ownerRepository.ImportDataForOwnerAsync(createStream.OpenReadStream(), idOwner);
            return Ok(wines);

            //return BadRequest("Cette owner n'est pas trouvable");
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
            var wines = await ownerRepository.GetAllPeakWineFromOwnerAsync(idOwner);
            if (wines != null)
                return Ok(wines);
            else
                return BadRequest("The caves's list don't exist");
        }

        /// <summary>
        /// List for all caves with theirs drawers and wines
        /// </summary>
        /// <param name="idOwner"></param>
        /// <returns></returns>
        [HttpGet("{idOwner}")]
        public async Task<ActionResult<List<Wine>>> GetAllWineFromOwner(int idOwner)
        {
            var wines = await ownerRepository.GetAllWineFromOwnerAsync(idOwner);
            if (wines != null)
                return Ok(wines);
            else
                return BadRequest("The cave's list don't exist");

        }
    }
}


//bool checkIsConnected = IsConnected();
//if (checkIsConnected)
//{

//}
//retrun BadRequest("Not logged");