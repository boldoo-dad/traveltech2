using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly IWebHostEnvironment environment;

        public AppController(IUnitOfWork uow, IMapper mapper, IWebHostEnvironment environment)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.environment = environment;
        }
        #region MenuItems
        [HttpGet("MenuItems")]
        public async Task<IActionResult> GetMenuItems()
        {
            var menuItems = await uow.MenuItemsRepository.getMenuItemsAsync();
            var menuItemsDto = mapper.Map<IEnumerable<MenuItemsDto>>(menuItems);
            return Ok(menuItemsDto);
        }
        [HttpGet("MenuItems/{id}")]
        public async Task<IActionResult> GetMenuItems(int id)
        {
            var menuItemFromDb = await uow.MenuItemsRepository.findMenuItemsAsync(id);
            var menuItemDto = mapper.Map<MenuItemsDto>(menuItemFromDb);
            return Ok(menuItemDto);
        }
        [HttpPost("MenuItems")]
        public async Task<IActionResult> PostMenuItems(MenuItemsDto menuItemsDto)
        {
            var menuItems = mapper.Map<MenuItems>(menuItemsDto);
            uow.MenuItemsRepository.addMenuItems(menuItems);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        [HttpPut("MenuItems/{id}")]
        public async Task<IActionResult> PutMenuItems(int id, MenuItemsDto menuItemsDto)
        {
            if (id != menuItemsDto.Id)
                return BadRequest("Update not allowed");
            var menuItemFromDb = await uow.MenuItemsRepository.findMenuItemsAsync(id);
            if (menuItemFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(menuItemsDto, menuItemFromDb);
            await uow.SaveAsync();
            return StatusCode(200);
        }
        [HttpDelete("MenuItems/{id}")]
        public async Task<IActionResult> DeleteMenuItems(int id)
        {
            var menuItemFromDb = await uow.MenuItemsRepository.findMenuItemsAsync(id);
            if (menuItemFromDb == null)
                return StatusCode(204);
            uow.MenuItemsRepository.deleteMenuItems(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        #endregion

        #region Menus
        [HttpGet("Menus")]
        public async Task<IActionResult> GetMenus()
        {
            var menus = await uow.MenusRepository.getMenusAsync();
            var menusDto = mapper.Map<IEnumerable<MenusDto>>(menus);
            return Ok(menusDto);
        }
        [HttpGet("Menus/{id}")]
        public async Task<IActionResult> GetMenus(int id)
        {
            var menuFromDb = await uow.MenusRepository.findMenusAsync(id);
            var menuDto = mapper.Map<MenusDto>(menuFromDb);
            return Ok(menuDto);
        }
        [HttpPost("Menus")]
        public async Task<IActionResult> PostMenus(MenusDto menusDto)
        {
            var menus = mapper.Map<Menus>(menusDto);
            uow.MenusRepository.addMenus(menus);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        [HttpPut("Menus/{id}")]
        public async Task<IActionResult> PutMenus(int id, MenusDto menusDto)
        {
            if (id != menusDto.Id)
                return BadRequest("Update not allowed");
            var menuFromDb = await uow.MenusRepository.findMenusAsync(id);
            if (menuFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(menusDto, menuFromDb);
            await uow.SaveAsync();
            return StatusCode(200);
        }
        [HttpDelete("Menus/{id}")]
        public async Task<IActionResult> DeleteMenus(int id)
        {
            var menuFromDb = await uow.MenusRepository.findMenusAsync(id);
            if (menuFromDb == null)
                return StatusCode(204);
            uow.MenusRepository.deleteMenus(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        #endregion

        #region Head
        [HttpGet("head")]
        public async Task<IActionResult> GetHead()
        {
            var head = await uow.HeadRepository.getHeadAsync();
            var headDto = mapper.Map<HeadDto>(new HeadDto()
            {
                Id = head.Id,
                Menus = head.Menus,
                ImageName = head.ImageName,
                ImageSrc = String.Format("{0}://{1}{2}/wwwroot/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, head.ImageName)
            });
            return Ok(headDto);
        }
        [HttpGet("head/{id}")]
        public async Task<IActionResult> GetHead(int id)
        {
            var headFromDb = await uow.HeadRepository.findHeadAsync(id);
            if (headFromDb == null)
                return StatusCode(204);
            headFromDb.ImageSrc = String.Format("{0}://{1}{2}/wwwroot/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, headFromDb.ImageName);
            var headsDto = mapper.Map<HeadDto>(headFromDb);
            return Ok(headsDto);
        }
        [HttpPost("head")]
        public async Task<IActionResult> PostHead([FromForm] HeadDto headDto)
        {
            var head = mapper.Map<Head>(headDto);
            if (headDto.ImageFile != null)
                head.ImageName = await ImageUploadAsync(headDto.ImageFile);
            uow.HeadRepository.addHead(head);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        [HttpPut("head/{id}")]
        public async Task<IActionResult> PutHead(int id, [FromForm] HeadDto headDto)
        {
            if (id != headDto.Id)
                return BadRequest("Update not allowed");
            var headFromDb = await uow.HeadRepository.findHeadAsync(id);
            if (headFromDb == null)
                return BadRequest("Update not allowed");
            if (headDto.ImageFile != null)
            {
                if (headFromDb.ImageName != null)
                    ImageDelete(headFromDb.ImageName);
                headFromDb.ImageName = await ImageUploadAsync(headDto.ImageFile);
                headDto.ImageName = headFromDb.ImageName;
            }
            mapper.Map(headDto, headFromDb);
            await uow.SaveAsync();
            return StatusCode(200);
        }
        [HttpPatch("head/{id}")]
        public async Task<IActionResult> PatchHead(int id, JsonPatchDocument<Head> headToPatch)
        {
            var headFromDb = await uow.HeadRepository.findHeadAsync(id);
            headToPatch.ApplyTo(headFromDb, ModelState);
            await uow.SaveAsync();
            return StatusCode(200);
        }

        [HttpDelete("head/{id}")]
        public async Task<IActionResult> DeleteHead(int id)
        {
            var headFromDb = await uow.HeadRepository.findHeadAsync(id);
            if (headFromDb == null)
                return StatusCode(204);
            if (headFromDb.ImageName != null)
                ImageDelete(headFromDb.ImageName);
            uow.HeadRepository.deleteHead(id);
            await uow.SaveAsync();
            return Ok(id);
        }

        #endregion

        #region App
        [HttpGet]
        public async Task<IActionResult> GetApp()
        {
            var app = await uow.AppRepository.getAppAsync();
            var appDto = mapper.Map<AppDto>(app);
            return Ok(appDto);
        }
        #endregion

        #region Image
        [NonAction]
        public async Task<string> ImageUploadAsync(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(environment.ContentRootPath, "wwwroot\\Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
        [NonAction]
        public void ImageDelete(string imageName)
        {
            var imagePath = Path.Combine(environment.ContentRootPath, "wwwroot\\Images", imageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            else
            {
                var imagePublishPath = Path.Combine(environment.ContentRootPath, "bin\\Release\netcoreapp3.1\\publish\\wwwroot\\Images", imageName);
                if (System.IO.File.Exists(imagePublishPath))
                    System.IO.File.Delete(imagePublishPath);
            }
        }
        #endregion
    }
}
