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

        public async Task<Head> findHeadAsync(int id)
        {
            return await dc.Heads.FindAsync(id);
        }

        public async Task<IEnumerable<Head>> getHeadsAsync()
        {
            return await dc.Heads.ToListAsync();
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
