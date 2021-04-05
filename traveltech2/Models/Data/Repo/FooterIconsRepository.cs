using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models.Data.Repo
{
    public class FooterIconsRepository : IFooterIconsRepository
    {
        private readonly DataContext dc;

        public FooterIconsRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void addFooterIcons(FooterIcons footerIcons)
        {
            dc.FooterIcons.Add(footerIcons);
        }

        public void deleteFooterIcons(int footerIconsId)
        {
            var id = dc.FooterIcons.Find(footerIconsId);
            dc.Remove(id);
        }

        public async Task<FooterIcons> findfFooterIconsAsync(int id)
        {
            return await dc.FooterIcons.FindAsync(id);
        }

        public async Task<IList<FooterIcons>> getFooterIconsAsync()
        {
            return await dc.FooterIcons.ToListAsync();
        }
    }
    public interface IFooterIconsRepository
    {
        void addFooterIcons(FooterIcons footerIcons);
        void deleteFooterIcons(int footerIconsId);
        Task<IList<FooterIcons>> getFooterIconsAsync();
        Task<FooterIcons> findfFooterIconsAsync(int id);

    }
}
