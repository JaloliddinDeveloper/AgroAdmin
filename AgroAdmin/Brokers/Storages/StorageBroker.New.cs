// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using AgroAdmin.Models.Foundations.News;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AgroAdmin.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<New> News { get; set; }

        public async ValueTask<New> InsertNewAsync(New New) =>
           await InsertAsync(New);

        public async ValueTask<IQueryable<New>> SelectAllNewsAsync() =>
            await SelectAllAsync<New>();

        public async ValueTask<New> SelectNewByIdAsync(int NewId) =>
            await SelectAsync<New>(NewId);

        public async ValueTask<New> UpdateNewAsync(New New) =>
            await UpdateAsync(New);

        public async ValueTask<New> DeleteNewAsync(New New) =>
            await DeleteAsync(New);

        public async ValueTask<IQueryable<New>> SelectAllNewsOrderAsync()
        {
            var allNews = await SelectAllAsync<New>();
            return allNews.OrderByDescending(news => news.Date);
        }
    }
}
