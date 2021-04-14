using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models.Data.Repo
{
    public class FooterMenusRepository : IFooterMenusRepository
    {
        private readonly DataContext dc;

        public FooterMenusRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void addFooterMenus(FooterMenus footerMenus)
        {
            dc.FooterMenus.Add(footerMenus);
        }

        public void deleteFooterMenus(int footerMenusId)
        {
            var id = dc.FooterMenus.Find(footerMenusId);
            dc.FooterMenus.Remove(id);
        }

        public async Task<FooterMenus> findfFooterMenusAsync(int id)
        {
            return await dc.FooterMenus.Include(m => m.Links).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IList<FooterMenus>> getFooterMenusAsync()
        {
            return await dc.FooterMenus.Include(m => m.Links).ToListAsync();
        }
    }
    public interface IFooterMenusRepository
    {
        void addFooterMenus(FooterMenus footerMenus);
        void deleteFooterMenus(int footerMenusId);
        Task<IList<FooterMenus>> getFooterMenusAsync();
        Task<FooterMenus> findfFooterMenusAsync(int id);
    }
}
