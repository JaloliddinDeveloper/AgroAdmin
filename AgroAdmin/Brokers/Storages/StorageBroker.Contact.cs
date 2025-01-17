// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using AgroAdmin.Models.Foundations.Contacts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AgroAdmin.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Contact> Contacts { get; set; }

        public async ValueTask<Contact> InsertContactAsync(Contact Contact) =>
           await InsertAsync(Contact);

        public async ValueTask<IQueryable<Contact>> SelectAllContactsAsync() =>
            await SelectAllAsync<Contact>();

        public async ValueTask<Contact> SelectContactByIdAsync(int ContactId) =>
            await SelectAsync<Contact>(ContactId);

        public async ValueTask<Contact> UpdateContactAsync(Contact Contact) =>
            await UpdateAsync(Contact);

        public async ValueTask<Contact> DeleteContactAsync(Contact Contact) =>
            await DeleteAsync(Contact);
    }
}
