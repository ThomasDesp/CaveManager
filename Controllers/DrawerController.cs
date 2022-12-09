﻿using CaveManager.Entities;
using CaveManager.Entities.DTO;
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

        [HttpPut]
        public async Task<ActionResult<Drawer>> UpdateDrawer(DTODrawer dTODrawer)
        {
            return Ok(drawerRepository.UpdateDrawerAsync(dTODrawer));
        }

        [HttpDelete]
        public async Task<ActionResult<Drawer>> DeleteDrawer(int Id)
        {
            return Ok(drawerRepository.RemoveDrawerAsync(Id));
        }









    }
}
