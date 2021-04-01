using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using traveltech2.Models;

namespace traveltech2.Controllers.Dto.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MenuItems, MenuItemsDto>().ReverseMap();
            CreateMap<Menus, MenusDto>().ReverseMap();
            CreateMap<Head, HeadDto>().ReverseMap();
            CreateMap<Head, HeadUpdateDto>().ReverseMap();
            CreateMap<App, AppDto>().ReverseMap();
        }
    }
}
