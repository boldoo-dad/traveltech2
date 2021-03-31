using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models.Data.Repo
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DataContext dc;

        public MenuRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void addMenu(Menu menu)
        {
            dc.Menus.Add(menu);
        }

        public void deleteMenu(int menuId)
        {
            var id = dc.Menus.Find(menuId);
            dc.Menus.Remove(id);
        }

        public async Task<Menu> findMenuAsync(int id)
        {
            return await dc.Menus.FindAsync(id);
        }

        public async Task<IEnumerable<Menu>> getMenusAsync()
        {
            return await dc.Menus.Include(m => m.Drop)
                .ToListAsync();
        }
    }
    public interface IMenuRepository
    {
        void addMenu(Menu menu);
        void deleteMenu(int menuId);
        Task<Menu> findMenuAsync(int id);
        Task<IEnumerable<Menu>> getMenusAsync();
    }
}
