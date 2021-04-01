using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models.Data.Repo
{
    public class AppRepository : IAppRepository
    {
        private readonly DataContext dc;

        public AppRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public async Task<App> getAppAsync()
        {
            return await dc.App.SingleOrDefaultAsync();
        }
    }
    public interface IAppRepository
    {
        Task<App> getAppAsync();
    }
}
