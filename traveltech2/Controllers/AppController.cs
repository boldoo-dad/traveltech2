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
        #region Drops
        [HttpGet("drops")]
        public async Task<IActionResult> GetDrops()
        {
            var drops = await uow.DropRepository.getDropsAsync();
            var dropsDto = mapper.Map<IEnumerable<DropDto>>(drops);
            return Ok(dropsDto);
        }
        [HttpGet("drops/{id}")]
        public async Task<IActionResult> GetDrop(int id)
        {
            var dropFromDb = await uow.DropRepository.findDropAsync(id);
            var dropDto = mapper.Map<DropDto>(dropFromDb);
            return Ok(dropDto);
        }
        [HttpPost("drops")]
        public async Task<IActionResult> AddDrop(DropDto dropDto)
        {
            var drop = mapper.Map<Drop>(dropDto);
            uow.DropRepository.addDrop(drop);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        [HttpPut("drops/{id}")]
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
        [HttpDelete("drops/{id}")]
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

        #region Menus
        [HttpGet("menus")]
        public async Task<IActionResult> GetMenus()
        {
            var menus = await uow.MenuRepository.getMenusAsync();
            var menusDto = mapper.Map<IEnumerable<MenuDto>>(menus);
            return Ok(menusDto);
        }
        [HttpGet("menus/{id}")]
        public async Task<IActionResult> GetMenu(int id)
        {
            var menuFromDb = await uow.MenuRepository.findMenuAsync(id);
            var menuDto = mapper.Map<MenuDto>(menuFromDb);
            return Ok(menuDto);
        }
        [HttpPost("menus")]
        public async Task<IActionResult> AddMenu(MenuDto menuDto)
        {
            var menu = mapper.Map<Menu>(menuDto);
            uow.MenuRepository.addMenu(menu);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        [HttpPut("menus/{id}")]
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
        [HttpDelete("menus/{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menuFromDb = await uow.MenuRepository.findMenuAsync(id);
            if (menuFromDb == null)
                return StatusCode(204);
            uow.MenuRepository.deleteMenu(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        #endregion

        #region Heads
        [HttpGet("heads")]
        public async Task<IActionResult> GetHeads()
        {
            var heads = await uow.HeadRepository.getHeadsAsync();
            var headsDto = mapper.Map<IEnumerable<HeadDto>>(heads.Select(m => new HeadDto()
            {
                Id = m.Id,
                Menu = m.Menu,
                ImageName = m.ImageName,
                ImageSrc = String.Format("{0}://{1}{2}/wwwroot/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, m.ImageName)
            }));
            return Ok(headsDto);
        }
        [HttpGet("heads/{id}")]
        public async Task<IActionResult> GetHead(int id)
        {
            var headFromDb = await uow.HeadRepository.findHeadAsync(id);
            if (headFromDb == null)
                return StatusCode(204);
            headFromDb.ImageSrc = String.Format("{0}://{1}{2}/wwwroot/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, headFromDb.ImageName);
            var headsDto = mapper.Map<HeadDto>(headFromDb);
            return Ok(headsDto);
        }
        [HttpPost("heads")]
        public async Task<IActionResult> PostHead([FromForm] HeadDto headDto)
        {
            var head = mapper.Map<Head>(headDto);
            if (headDto.ImageFile != null)
                head.ImageName = await ImageUploadAsync(headDto.ImageFile);
            uow.HeadRepository.addHead(head);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        [HttpPut("heads/{id}")]
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
        [HttpPatch("heads/{id}")]
        public async Task<IActionResult> PatchHead(int id, JsonPatchDocument<Head> headToPatch)
        {
            var headFromDb = await uow.HeadRepository.findHeadAsync(id);
            headToPatch.ApplyTo(headFromDb, ModelState);
            await uow.SaveAsync();
            return StatusCode(200);
        }

        [HttpDelete("heads/{id}")]
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
