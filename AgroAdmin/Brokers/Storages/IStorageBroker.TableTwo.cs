// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using AgroAdmin.Models.Foundations.ProductTwos;

namespace AgroAdmin.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<TableTwo> InsertTableTwoAsync(TableTwo tableTwo);
        ValueTask<IQueryable<TableTwo>> SelectAllTableTwosAsync();
        ValueTask<TableTwo> SelectTableTwoByIdAsync(int tableTwoId);
        ValueTask<TableTwo> UpdateTableTwoAsync(TableTwo tableTwo);
        ValueTask<TableTwo> DeleteTableTwoAsync(TableTwo tableTwo);

        Task<IEnumerable<TableTwo>> GetTableTwosProOneByIdAsync(int proTwoId);
    }
}
