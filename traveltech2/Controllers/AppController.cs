using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using traveltech2.Controllers.Dto;
using traveltech2.Models;
using traveltech2.Models.Data;

namespace traveltech2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public AppController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        #region
        [HttpGet("drop")]
        public async Task<IActionResult> GetDrops()
        {
            var drops = await uow.DropRepository.getDropsAsync();
            var dropsDto = mapper.Map<IEnumerable<DropDto>>(drops);
            return Ok(dropsDto);
        }
        [HttpGet("drop/{id}")]
        public async Task<IActionResult> GetDrop(int id)
        {
            var dropFromDb = await uow.DropRepository.findDropAsync(id);
            var dropDto = mapper.Map<DropDto>(dropFromDb);
            return Ok(dropDto);
        }
        [HttpPost("drop")]
        public async Task<IActionResult> AddDrop(DropDto dropDto)
        {
            var drop = mapper.Map<Drop>(dropDto);
            uow.DropRepository.addDrop(drop);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        [HttpPut("drop/{id}")]
        public async Task<IActionResult> UpdateDrop(int id, DropDto dropDto)
        {
            if (id != dropDto.Id)
                return BadRequest("Update not allowed");
            var dropFromDb = await uow.DropRepository.findDropAsync(id);
            if (dropFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(dropDto, dropFromDb);
            await uow.SaveAsync();
            return StatusCode(200);
        }
        [HttpPatch("drop/{id}")]
        public async Task<IActionResult> UpdateDropPatch(int id, JsonPatchDocument<Drop> dropToPatch)
        {
            var dropFromDb = await uow.DropRepository.findDropAsync(id);
            dropToPatch.ApplyTo(dropFromDb, ModelState);
            await uow.SaveAsync();
            return StatusCode(200);
        }
        [HttpDelete("drop/{id}")]
        public async Task<IActionResult> DeleteDrop(int id)
        {
            var dropFromDb = await uow.DropRepository.findDropAsync(id);
            if (dropFromDb == null)
                return StatusCode(204);
            uow.DropRepository.deleteDrop(id);
            await uow.SaveAsync();
            return Ok(id);
        }


        #endregion
        [HttpGet("menu")]
        public async Task<IActionResult> GetMenus()
        {
            var menus = await uow.MenuRepository.getMenusAsync();
            var menusDto = mapper.Map<IEnumerable<MenuDto>>(menus);
            return Ok(menusDto);
        }
        [HttpGet("menu/{id}")]
        public async Task<IActionResult> GetMenu(int id)
        {
            var menuFromDb = await uow.MenuRepository.findMenuAsync(id);
            var menuDto = mapper.Map<MenuDto>(menuFromDb);
            return Ok(menuDto);
        }
        [HttpPost("menu")]
        public async Task<IActionResult> AddMenu(MenuDto menuDto)
        {
            var menu = mapper.Map<Menu>(menuDto);
            uow.MenuRepository.addMenu(menu);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        [HttpPut("menu/{id}")]
        public async Task<IActionResult> UpdateMenu(int id, MenuDto menuDto)
        {
            if (id != menuDto.Id)
                return BadRequest("Update not allowed");
            var menuFromDb = await uow.MenuRepository.findMenuAsync(id);
            if (menuFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(menuDto, menuFromDb);
            await uow.SaveAsync();
            return StatusCode(200);
        }
        [HttpPatch("menu/{id}")]
        public async Task<IActionResult> UpdateMenuPatch(int id, JsonPatchDocument<Menu> menuToPatch)
        {
            var menuFromDb = await uow.MenuRepository.findMenuAsync(id);
            menuToPatch.ApplyTo(menuFromDb, ModelState);
            await uow.SaveAsync();
            return StatusCode(200);
        }
        [HttpDelete("menu/{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menuFromDb = await uow.MenuRepository.findMenuAsync(id);
            if (menuFromDb == null)
                return StatusCode(204);
            uow.MenuRepository.deleteMenu(id);
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}
