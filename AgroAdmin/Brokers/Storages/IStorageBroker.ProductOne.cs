// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using AgroAdmin.Models.Foundations.ProductOnes;
using System.Linq;
using System.Threading.Tasks;

namespace AgroAdmin.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<ProductOne> InsertProductOneAsync(ProductOne productOne);
        ValueTask<IQueryable<ProductOne>> SelectAllProductOnesAsync();
        ValueTask<ProductOne> SelectProductOneByIdAsync(int productOneId);
        ValueTask<ProductOne> UpdateProductOneAsync(ProductOne productOne);
        ValueTask<ProductOne> DeleteProductOneAsync(ProductOne productOne);
    }
}
