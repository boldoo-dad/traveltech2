using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models.Data.Repo
{
    public class MenuItemsRepository : IMenuItemsRepository
    {
        private readonly DataContext dc;

        public MenuItemsRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void addMenuItems(MenuItems menuItems)
        {
            dc.MenuItems.Add(menuItems);
        }

        public void deleteMenuItems(int menuItemsId)
        {
            var id = dc.MenuItems.Find(menuItemsId);
            dc.MenuItems.Remove(id);
        }

        public async Task<MenuItems> findMenuItemsAsync(int id)
        {
           return await dc.MenuItems.Include(m => m.Links).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MenuItems>> getMenuItemsAsync()
        {
            return await dc.MenuItems.Include(m=>m.Links).ToListAsync();
        }
    }
    public interface IMenuItemsRepository
    {
        void addMenuItems(MenuItems menuItems);
        void deleteMenuItems(int menuItemsId);
        Task<MenuItems> findMenuItemsAsync(int id);
        Task<IEnumerable<MenuItems>> getMenuItemsAsync();

    }
}
