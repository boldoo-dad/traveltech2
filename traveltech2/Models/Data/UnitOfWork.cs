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
        public IMenuItemsRepository MenuItemsRepository =>
            new Repo.MenuItemsRepository(dc);

        public IMenusRepository MenusRepository =>
            new MenusRepository(dc);

        public IHeadRepository HeadRepository =>
            new HeadRepository(dc);

        public IAppRepository AppRepository =>
            new AppRepository(dc);

        public ILinksRepository LinksRepository =>
            new LinksRepository(dc);
        public IFooterRepository FooterRepository =>
            new FooterRepository(dc);

        public IFooterIconsRepository FooterIconsRepository =>
            new FooterIconsRepository(dc);

        public async Task<bool> SaveAsync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
    public interface IUnitOfWork
    {
        public IFooterIconsRepository FooterIconsRepository { get; }
        public IFooterRepository FooterRepository { get; }
        public ILinksRepository LinksRepository { get; }
        public IMenuItemsRepository MenuItemsRepository { get; }
        public IMenusRepository MenusRepository { get; }
        public IHeadRepository HeadRepository { get; }
        public IAppRepository AppRepository { get; }
        Task<bool> SaveAsync();
    }
}
