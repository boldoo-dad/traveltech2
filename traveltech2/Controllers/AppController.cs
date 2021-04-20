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

        public AppController(IUnitOfWork uow,
                             IMapper mapper,
                             IWebHostEnvironment environment)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.environment = environment;
        }

        #region FooterMenus
        [HttpGet("FooterMenus")]
        public async Task<IActionResult> GetFooterMenus()
        {
            var footerMenus = await uow.FooterMenusRepository.getFooterMenusAsync();
            var footerMenusDto = mapper.Map<IEnumerable<FooterMenusDto>>(footerMenus);
            return Ok(footerMenusDto);
        }
        [HttpGet("FooterMenus/{id}")]
        public async Task<IActionResult> GetFooterMenus(int id)
        {
            var footerMenuFromDb = await uow.FooterMenusRepository.findfFooterMenusAsync(id);
            var footerMenuDto = mapper.Map<FooterMenusDto>(footerMenuFromDb);
            return Ok(footerMenuDto);
        }
        [HttpPost("FooterMenus")]
        public async Task<IActionResult> PostFooterMenus(FooterMenusDto footerMenusDto)
        {
            var linksFromDto = footerMenusDto.Links;
            footerMenusDto.Links = new List<LinksDto>();


            var footerMenus = mapper.Map<FooterMenus>(footerMenusDto);
            uow.FooterMenusRepository.addFooterMenus(footerMenus);

            foreach (var link in linksFromDto)
            {
                var link1 = uow.LinksRepository.findLinksAsync(link.Id).Result;
                footerMenus.Links.Add(link1);
                link1.FooterMenus.Add(footerMenus);
            }
            await uow.SaveAsync();

            return StatusCode(201);
        }
        [HttpPut("FooterMenus/{id}")]
        public async Task<IActionResult> PutMenuItems(int id, FooterMenusDto footerMenusDto)
        {
            if (id != footerMenusDto.Id)
                return BadRequest("Update not allowed");
            var footermenuFromDb = await uow.FooterMenusRepository.findfFooterMenusAsync(id);
            if (footermenuFromDb == null)
                return BadRequest("Update not allowed");

            var linksFromDto = footerMenusDto.Links;
            footerMenusDto.Links = new List<LinksDto>();

            var footerMenus = mapper.Map(footerMenusDto, footermenuFromDb);
            foreach (var link in linksFromDto)
            {
                var link1 = uow.LinksRepository.findLinksAsync(link.Id).Result;
                footerMenus.Links.Add(link1);
                link1.FooterMenus.Add(footerMenus);
            }
            await uow.SaveAsync();
            return StatusCode(200);
        }
        [HttpDelete("FooterMenus/{id}")]
        public async Task<IActionResult> DeleteFooterMenus(int id)
        {
            var footerMenuFromDb = await uow.FooterMenusRepository.findfFooterMenusAsync(id);
            if (footerMenuFromDb == null)
                return StatusCode(204);
            uow.FooterMenusRepository.deleteFooterMenus(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        #endregion

        #region FooterIcons
        [HttpGet("FooterIcons")]
        public async Task<IActionResult> GetFooterIcons()
        {
            var footerIcons = await uow.FooterIconsRepository.getFooterIconsAsync();
            var footerIconsDto = mapper.Map<IList<FooterIconsDto>>(footerIcons.Select(m => new FooterIconsDto()
            {
                Id = m.Id,
                Url = m.Url,
                FooterID = m.FooterID,
                ImageName = m.ImageName,
                ImageSrc = String.Format("{0}://{1}{2}/wwwroot/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, m.ImageName)
            }));
            return Ok(footerIconsDto);
        }
        [HttpGet("FooterIcons/{id}")]
        public async Task<IActionResult> GetFooterIcon(int id)
        {
            var footerIconsFromDb = await uow.FooterIconsRepository.findfFooterIconsAsync(id);
            footerIconsFromDb.ImageSrc = String.Format("{0}://{1}{2}/wwwroot/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, footerIconsFromDb.ImageName);
            var footerIconsDto = mapper.Map<FooterIconsDto>(footerIconsFromDb);
            return Ok(footerIconsDto);
        }
        [HttpPost("FooterIcons")]
        public async Task<IActionResult> PostFooterIcons([FromForm] FooterIconsDto footerIconsDto)
        {
            var footerIcons = mapper.Map<FooterIcons>(footerIconsDto);
            if (footerIconsDto.ImageFile != null)
                footerIcons.ImageName = await ImageUploadAsync(footerIconsDto.ImageFile);
            uow.FooterIconsRepository.addFooterIcons(footerIcons);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        [HttpPut("FooterIcons/{id}")]
        public async Task<IActionResult> PutFooterIcons(int id, [FromForm] FooterIconsDto footerIconsDto)
        {
            if (id != footerIconsDto.Id)
                return BadRequest("Update not allowed");
            var footerIconsFromDb = await uow.FooterIconsRepository.findfFooterIconsAsync(id);
            if (footerIconsFromDb == null)
                return BadRequest("Update not allowed");
            if (footerIconsDto.ImageFile != null)
            {
                if (footerIconsFromDb.ImageName != null)
                    ImageDelete(footerIconsFromDb.ImageName);
                footerIconsFromDb.ImageName = await ImageUploadAsync(footerIconsDto.ImageFile);
                footerIconsDto.ImageName = footerIconsFromDb.ImageName;
            }
            mapper.Map(footerIconsDto, footerIconsFromDb);
            await uow.SaveAsync();
            return StatusCode(200);
        }
        [HttpDelete("FooterIcons/{id}")]
        public async Task<IActionResult> DeleteFooterIcons(int id)
        {
            var footerIconsFromDb = await uow.FooterIconsRepository.findfFooterIconsAsync(id);
            if (footerIconsFromDb == null)
                return StatusCode(204);
            if (footerIconsFromDb.ImageName != null)
                ImageDelete(footerIconsFromDb.ImageName);
            uow.FooterIconsRepository.deleteFooterIcons(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        #endregion

        #region Footer
        [HttpGet("Footer")]
        public async Task<IActionResult> GetFooter()
        {
            var footer = await uow.FooterRepository.getFooterAsync();
            var footerDto = mapper.Map<FooterDto>(footer);
            return Ok(footerDto);
        }
        [HttpPut("Footer/{id}")]
        public async Task<IActionResult> PutFooter(int id, FooterUpdateDto footerUpdateDto)
        {
            if (id != footerUpdateDto.Id)
                return BadRequest("Update not allowed");
            var footerFromDb = await uow.FooterRepository.findFooterAsync(id);
            if (footerFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(footerUpdateDto, footerFromDb);
            await uow.SaveAsync();
            return StatusCode(200);
        }
        #endregion

        #region Links
        [HttpGet("Links")]
        public async Task<IActionResult> GetLinks()
        {
            var links = await uow.LinksRepository.getLinksAsync();
            var linksDto = mapper.Map<IEnumerable<LinksDto>>(links);
            return Ok(linksDto);
        }
        [HttpGet("Links/{id}")]
        public async Task<IActionResult> GetLink(int id)
        {
            var linksFromDb = await uow.LinksRepository.findLinksAsync(id);
            var linksDto = mapper.Map<LinksDto>(linksFromDb);
            return Ok(linksDto);
        }
        [HttpPost("Links")]
        public async Task<IActionResult> PostLinks(LinksDto linksDto)
        {
            var links = mapper.Map<Links>(linksDto);
            uow.LinksRepository.addLinks(links);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        [HttpPut("Links/{id}")]
        public async Task<IActionResult> PutLinks(int id, LinksDto linksDto)
        {
            if (id != linksDto.Id)
                return BadRequest("Update not allowed");
            var linksFromDb = await uow.LinksRepository.findLinksAsync(id);
            if (linksFromDb == null)
                return BadRequest("Update not allowed");
            mapper.Map(linksDto, linksFromDb);
            await uow.SaveAsync();
            return StatusCode(200);
        }
        [HttpDelete("Links/{id}")]
        public async Task<IActionResult> DeleteLinks(int id)
        {
            var linksFromDb = await uow.LinksRepository.findLinksAsync(id);
            if (linksFromDb == null)
                return StatusCode(204);
            uow.LinksRepository.deleteLinks(id);
            await uow.SaveAsync();
            return Ok(id);
        }
        #endregion

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
            var linksFromDto = menuItemsDto.Links;
            menuItemsDto.Links = new List<LinksDto>();

            var menuItems = mapper.Map<MenuItems>(menuItemsDto);
            uow.MenuItemsRepository.addMenuItems(menuItems);

            foreach (var link in linksFromDto)
            {
                var link1 = uow.LinksRepository.findLinksAsync(link.Id).Result;
                menuItems.Links.Add(link1);
                link1.MenuItems.Add(menuItems);
            }
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

            var linksFromDto = menuItemsDto.Links;
            menuItemsDto.Links = new List<LinksDto>();

            var menuItems = mapper.Map(menuItemsDto, menuItemFromDb);
            foreach (var link in linksFromDto)
            {
                var link1 = uow.LinksRepository.findLinksAsync(link.Id).Result;
                menuItems.Links.Add(link1);
                link1.MenuItems.Add(menuItems);
            }
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
                Color = head.Color,
                Menus = head.Menus,
                ImageName = head.ImageName,
                ImageSrc = String.Format("{0}://{1}{2}/wwwroot/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, head.ImageName)
            });
            return Ok(headDto);
        }
        [HttpPut("head/{id}")]
        public async Task<IActionResult> PutHead(int id, [FromForm] HeadUpdateDto headDto)
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
