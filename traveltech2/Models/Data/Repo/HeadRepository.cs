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
            dc.Heads.Add(head);
        }

        public void deleteHead(int headId)
        {
            var id = dc.Heads.Find(headId);
            dc.Heads.Remove(id);
        }

        //private Head getById(int id)
        //{
        //    return dc.Heads.Include(x => x.Menu).FirstOrDefault(x => x.Id == id);
        //}

        public async Task<Head> findHeadAsync(int id)
        {
            //return await Task.Run(()=>getById(id));
            //return await dc.Heads.FindAsync(id);
            return await dc.Heads.Include(x => x.Menu).ThenInclude(s => s.Drop).FirstOrDefaultAsync(y => y.Id == id);
        }

        public async Task<IEnumerable<Head>> getHeadsAsync()
        {
            return await dc.Heads.Include("Menu.Drop").ToListAsync();
        }
    }
    public interface IHeadRepository
    {
        void addHead(Head head);
        void deleteHead(int headId);
        Task<Head> findHeadAsync(int id);
        Task<IEnumerable<Head>> getHeadsAsync();
    }
}
