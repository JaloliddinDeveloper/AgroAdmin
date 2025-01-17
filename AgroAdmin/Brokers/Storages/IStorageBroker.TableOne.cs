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
        ValueTask<TableOne> InsertTableOneAsync(TableOne TableOne);
        ValueTask<IQueryable<TableOne>> SelectAllTableOnesAsync();
        ValueTask<TableOne> SelectTableOneByIdAsync(int TableOneId);
        ValueTask<TableOne> UpdateTableOneAsync(TableOne TableOne);
        ValueTask<TableOne> DeleteTableOneAsync(TableOne TableOne);
    }
}
