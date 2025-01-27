// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using AgroAdmin.Models.Foundations.ProductTwos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AgroAdmin.Models.Foundations.ProductOnes;

namespace AgroAdmin.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<TableTwo> TableTwos { get; set; }

        public async ValueTask<TableTwo> InsertTableTwoAsync(TableTwo tableTwo)=>
            await InsertAsync(tableTwo);
       
        public async ValueTask<IQueryable<TableTwo>> SelectAllTableTwosAsync()=>
            await SelectAllAsync<TableTwo>();
      
        public async ValueTask<TableTwo> SelectTableTwoByIdAsync(int tableTwoId)=>
            await SelectAsync<TableTwo>(tableTwoId);
        
        public async ValueTask<TableTwo> UpdateTableTwoAsync(TableTwo tableTwo)=>
            await UpdateAsync(tableTwo);
        
        public async ValueTask<TableTwo> DeleteTableTwoAsync(TableTwo tableTwo)=>
            await DeleteAsync(tableTwo);

        public async Task<IEnumerable<TableTwo>> GetTableTwosProOneByIdAsync(int proTwoId)
        {
            return await this.TableTwos
                .Where(b => b.ProductTwoId == proTwoId)
                .ToListAsync();
        }
    }
}
