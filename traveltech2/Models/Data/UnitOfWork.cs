using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using traveltech2.Models.Data.Repo;

namespace traveltech2.Models.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dc;

        public UnitOfWork(DataContext dc)
        {
            this.dc = dc;
        }
        public IDropRepository DropRepository => 
            new DropRepository(dc);

        public IMenuRepository MenuRepository => 
            new MenuRepository(dc);

        public async Task<bool> SaveAsync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
    public interface IUnitOfWork
    {
        public IDropRepository DropRepository { get; }
        public IMenuRepository MenuRepository { get; }
        Task<bool> SaveAsync();
    }
}
