using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using traveltech2.Models;

namespace traveltech2.Models.Data.Repo
{
    public class LinksRepository : ILinksRepository
    {
        private readonly DataContext dc;

        public LinksRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void addLinks(Links links)
        {
            dc.Links.Add(links);
        }

        public void deleteLinks(int linksId)
        {
            var id = dc.Links.Find(linksId);
            dc.Remove(id);
        }

        public async Task<Links> findLinksAsync(int id)
        {
            return await dc.Links.Include(x => x.MenuItems).FirstOrDefaultAsync(y => y.Id == id);
        }
        public async Task<IEnumerable<Links>> getLinksAsync()
        {
            return await dc.Links.Include(m => m.MenuItems).ToListAsync();
        }
    }

}
public interface ILinksRepository
{
    void addLinks(Links links);
    void deleteLinks(int linksId);
    Task<Links> findLinksAsync(int id);
    Task<IEnumerable<Links>> getLinksAsync();
}

