using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models.Data.Repo
{
    public class FooterRepository : IFooterRepository
    {
        private readonly DataContext dc;

        public FooterRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void add(Footer footer)
        {
            dc.Footer.Add(footer);
        }

        public void delete(int footerId)
        {
            var id = dc.Footer.Find(footerId);
            dc.Remove(id);
        }


        public async Task<Footer> findFooterAsync(int id)
        {
            //return await dc.Footer.FindAsync(id);
            return await dc.Footer
                .Include(m => m.FooterIcons)
                .Include("FooterMenus.Links")
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Footer> getFooterAsync()
        {
            return await dc.Footer
                .Include(m => m.FooterIcons)
                .Include("FooterMenus.Links")
                .FirstOrDefaultAsync();
        }
    }
    public interface IFooterRepository
    {
        void add(Footer footer);
        void delete(int footerId);
        Task<Footer> getFooterAsync();
        Task<Footer> findFooterAsync(int id);
    }
}
