using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models.Data.Repo
{
    public class HeadRepository : IHeadRepository
    {
        private readonly DataContext dc;

        public HeadRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void addHead(Head head)
        {
            dc.Head.Add(head);
        }

        public void deleteHead(int headId)
        {
            var id = dc.Head.Find(headId);
            dc.Head.Remove(id);
        }

        //private Head getById(int id)
        //{
        //    return dc.Heads.Include(x => x.Menu).FirstOrDefault(x => x.Id == id);
        //}

        public async Task<Head> findHeadAsync(int id)
        {
            //return await Task.Run(()=>getById(id));
            //return await dc.Heads.Include(x => x.Menus).FindAsync(id);
          //  return await dc.Head.Include(x => x.Menus).ThenInclude(s => s.MenuItems).ThenInclude(x=>x.Links).FirstOrDefaultAsync(y => y.Id == id);
            return await dc.Head.Include("Menus.MenuItems.Links").FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Head> getHeadAsync()
        {
            return await dc.Head.Include("Menus.MenuItems.Links").FirstOrDefaultAsync();
        }
    }
    public interface IHeadRepository
    {
        void addHead(Head head);
        void deleteHead(int headId);
        Task<Head> findHeadAsync(int id);
        Task<Head> getHeadAsync();
    }
}
