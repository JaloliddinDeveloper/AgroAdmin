// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using AgroAdmin.Models.Foundations.Photos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AgroAdmin.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Photo> Photos { get; set; }

        public async ValueTask<Photo> InsertPhotoAsync(Photo Photo) =>
         await InsertAsync(Photo);

        public async ValueTask<IQueryable<Photo>> SelectAllPhotosAsync() =>
            await SelectAllAsync<Photo>();

        public async ValueTask<Photo> SelectPhotoByIdAsync(int PhotoId) =>
            await SelectAsync<Photo>(PhotoId);

        public async ValueTask<Photo> UpdatePhotoAsync(Photo Photo) =>
            await UpdateAsync(Photo);

        public async ValueTask<Photo> DeletePhotoAsync(Photo Photo) =>
            await DeleteAsync(Photo);

        public async ValueTask<IQueryable<Photo>> SelectAllPhotosOrderAsync()
        {
            var allPhotos = await SelectAllAsync<Photo>();
            return allPhotos.OrderByDescending(Photos => Photos.CreateDate);
        }
    }
}
