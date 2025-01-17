// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using AgroAdmin.Models.Foundations.ProductTwos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AgroAdmin.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<ProductTwo> ProductTwos { get; set; }

        public async ValueTask<ProductTwo> InsertProductTwoAsync(ProductTwo productTwo)=>
            await InsertAsync(productTwo);

        public async ValueTask<IQueryable<ProductTwo>> SelectAllProductTwosAsync() =>
            await SelectAllAsync<ProductTwo>();

        public async ValueTask<ProductTwo> SelectProductTwoByIdAsync(int productTwoId) =>
            await SelectAsync<ProductTwo>(productTwoId);
        
        public async ValueTask<ProductTwo> UpdateProductTwoAsync(ProductTwo productTwo)=>
            await UpdateAsync(productTwo);
        
        public async ValueTask<ProductTwo> DeleteProductTwoAsync(ProductTwo productTwo)=>
            await DeleteAsync(productTwo);
       
    }
}
