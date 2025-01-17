// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using AgroAdmin.Models.Foundations.Photos;
using System.Linq;
using System.Threading.Tasks;

namespace AgroAdmin.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Photo> InsertPhotoAsync(Photo Photo);
        ValueTask<IQueryable<Photo>> SelectAllPhotosAsync();
        ValueTask<Photo> SelectPhotoByIdAsync(int PhotoId);
        ValueTask<Photo> UpdatePhotoAsync(Photo Photo);
        ValueTask<Photo> DeletePhotoAsync(Photo Photo);

        ValueTask<IQueryable<Photo>> SelectAllPhotosOrderAsync();
    }
}
