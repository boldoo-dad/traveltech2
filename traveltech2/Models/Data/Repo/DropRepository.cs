using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models.Data.Repo
{
    public class DropRepository : IDropRepository
    {
        private readonly DataContext dc;

        public DropRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void addDrop(Drop drop)
        {
            dc.Drops.Add(drop);
        }

        public void deleteDrop(int dropId)
        {
            var id = dc.Drops.Find(dropId);
            dc.Drops.Remove(id);
        }

        public async Task<Drop> findDropAsync(int id)
        {
            return await dc.Drops.FindAsync(id);
        }

        public async Task<IEnumerable<Drop>> getDropsAsync()
        {
            return await dc.Drops.ToListAsync();
        }
    }
    public interface IDropRepository
    {
        void addDrop(Drop drop);
        void deleteDrop(int dropId);
        Task<Drop> findDropAsync(int id);
        Task<IEnumerable<Drop>> getDropsAsync();

    }
}
