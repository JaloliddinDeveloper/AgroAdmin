// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using AgroAdmin.Models.Foundations.ProductTwos;
using System.Linq;
using System.Threading.Tasks;

namespace AgroAdmin.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<ProductTwo> InsertProductTwoAsync(ProductTwo productTwo);
        ValueTask<IQueryable<ProductTwo>> SelectAllProductTwosAsync();
        ValueTask<ProductTwo> SelectProductTwoByIdAsync(int productTwoId);
        ValueTask<ProductTwo> UpdateProductTwoAsync(ProductTwo productTwo);
        ValueTask<ProductTwo> DeleteProductTwoAsync(ProductTwo productTwo);
    }
}
