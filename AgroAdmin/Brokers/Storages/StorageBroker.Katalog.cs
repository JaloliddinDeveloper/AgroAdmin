// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using AgroAdmin.Models.Foundations.Katalogs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AgroAdmin.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Katalog> Katalogs { get; set; }

        public async ValueTask<Katalog> InsertKatalogAsync(Katalog Katalog) =>
           await InsertAsync(Katalog);

        public async ValueTask<IQueryable<Katalog>> SelectAllKatalogsAsync() =>
            await SelectAllAsync<Katalog>();

        public async ValueTask<Katalog> SelectKatalogByIdAsync(int KatalogId) =>
            await SelectAsync<Katalog>(KatalogId);

        public async ValueTask<Katalog> UpdateKatalogAsync(Katalog Katalog) =>
            await UpdateAsync(Katalog);

        public async ValueTask<Katalog> DeleteKatalogAsync(Katalog Katalog) =>
            await DeleteAsync(Katalog);
    }
}
