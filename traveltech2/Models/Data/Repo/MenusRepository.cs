using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models.Data.Repo
{
    public class MenusRepository : IMenusRepository
    {
        private readonly DataContext dc;

        public MenusRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void addMenus(Menus menus)
        {
            dc.Menus.Add(menus);
        }

        public void deleteMenus(int menusId)
        {
            var id = dc.Menus.Find(menusId);
            dc.Menus.Remove(id);
        }

        public async Task<Menus> findMenusAsync(int id)
        {
            return await dc.Menus.FindAsync(id);
        }

        public async Task<IEnumerable<Menus>> getMenusAsync()
        {
            return await dc.Menus.Include(m => m.MenuItems)
                .ToListAsync();
        }
    }
    public interface IMenusRepository
    {
        void addMenus(Menus menus);
        void deleteMenus(int menusId);
        Task<Menus> findMenusAsync(int id);
        Task<IEnumerable<Menus>> getMenusAsync();
    }
}
