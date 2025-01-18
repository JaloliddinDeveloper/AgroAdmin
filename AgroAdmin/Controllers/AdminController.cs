// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using AgroAdmin.Brokers.Storages;
using AgroAdmin.Models.Foundations.News;
using AgroAdmin.Models.Foundations.Photos;
using Microsoft.AspNetCore.Mvc;

namespace AgroAdmin.Controllers
{
    public class AdminController: Controller
    {
        private readonly IStorageBroker storageBroker;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AdminController(
            IStorageBroker storageBroker,
            IWebHostEnvironment webHostEnvironment)
        {
            this.storageBroker = storageBroker;
            this.webHostEnvironment = webHostEnvironment;
        }

        public ActionResult Index()=>
             View();
       
        //Yangilik
        [HttpGet]
        public async ValueTask<IActionResult> Yangilik()
        { 
            var newss=await this.storageBroker.SelectAllNewsAsync();

            return View(newss);
        }

        [HttpGet]
        public async ValueTask<IActionResult> AddYangilik()=>
             View();
        
        [HttpPost]
        public async Task<IActionResult> AddYangilik(New yangilik, IFormFile uploadedImage)
        {
            try
            {
                if (uploadedImage != null && uploadedImage.Length > 0)
                {
                    string uniqueFileName = $"{Guid.NewGuid()}_{uploadedImage.FileName}";
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    var directoryPath = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadedImage.CopyToAsync(fileStream);
                    }

                    yangilik.NewPicture = $"/images/{uniqueFileName}";
                    yangilik.Date = DateTimeOffset.Now;
                }

                await this.storageBroker.InsertNewAsync(yangilik);
                return RedirectToAction("Yangilik");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while adding the news item.";
            }
            return View();
        }
         
        public async ValueTask<IActionResult> EditYangilik(int id)
        {
            var yangilik = await this.storageBroker.SelectNewByIdAsync(id);
            return View(yangilik);
        }

        [HttpPost]
        public async Task<IActionResult> EditYangilik(New updatedNew, IFormFile uploadedImage)
        {
            if (updatedNew == null)
            {
                return BadRequest("Invalid news data.");
            }

            if (uploadedImage != null && uploadedImage.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(uploadedImage.FileName) +
                               "_" + Guid.NewGuid().ToString() + Path.GetExtension(uploadedImage.FileName);

                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(uploadsFolder, fileName);

                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadedImage.CopyToAsync(stream);
                }

                updatedNew.NewPicture = $"/images/{fileName}";
            }
            else if (string.IsNullOrEmpty(updatedNew.NewPicture))
            {
                var existingNew = await this.storageBroker.SelectNewByIdAsync(updatedNew.Id);
                if (existingNew != null)
                {
                    updatedNew.NewPicture = existingNew.NewPicture;
                }
            }

            var existingNewToUpdate = await this.storageBroker.SelectNewByIdAsync(updatedNew.Id);
            if (existingNewToUpdate == null)
            {
                return NotFound("News item not found.");
            }

            existingNewToUpdate.TitleUz = updatedNew.TitleUz;
            existingNewToUpdate.TitleRu = updatedNew.TitleRu;
            existingNewToUpdate.DesUz = updatedNew.DesUz;
            existingNewToUpdate.DesRu = updatedNew.DesRu;
            existingNewToUpdate.DescribtionUz = updatedNew.DescribtionUz;
            existingNewToUpdate.DescribtionRu = updatedNew.DescribtionRu;
            existingNewToUpdate.NewPicture = updatedNew.NewPicture;
            existingNewToUpdate.Date = DateTimeOffset.Now;

            try
            {
                await this.storageBroker.UpdateNewAsync(existingNewToUpdate);
                TempData["Message"] = "News item successfully updated!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the news item.";
            }

            return RedirectToAction("Yangilik");
        }

        [HttpPost]
        public async ValueTask<IActionResult> DeleteYangilik(int id)
        {
            var newsItem = await this.storageBroker.SelectNewByIdAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }

           await this.storageBroker.DeleteNewAsync(newsItem);

            return RedirectToAction("Yangilik");
        }

        [HttpGet]
        public async ValueTask<IActionResult> Photo()
        {
            var photos = await this.storageBroker.SelectAllPhotosAsync();
            return View(photos);
        }

        //PHOTO 
        public async ValueTask<IActionResult> AddPhoto()=>
             View();
        
        [HttpPost]
        public async Task<IActionResult> AddPhoto(Photo photo, IFormFile uploadedImage)
        {
            try
            {
                if (uploadedImage != null && uploadedImage.Length > 0)
                {
                    string uniqueFileName = $"{Guid.NewGuid()}_{uploadedImage.FileName}";
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    var directoryPath = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadedImage.CopyToAsync(fileStream);
                    }

                    photo.PictureUrl = $"/images/{uniqueFileName}";
                    photo.CreateDate = DateTimeOffset.Now;
                }

                await this.storageBroker.InsertPhotoAsync(photo);
                return RedirectToAction("Photo");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while adding the news item.";
            }
            return View();
        }

        public async ValueTask<IActionResult> EditPhoto(int id)
        {
            var photo = await this.storageBroker.SelectPhotoByIdAsync(id);
            return View(photo);
        }

        [HttpPost]
        public async Task<IActionResult> EditPhoto(Photo updatedPhoto, IFormFile uploadedImage)
        {
            if (updatedPhoto == null)
            {
                return BadRequest("Invalid photo data.");
            }

            if (uploadedImage != null && uploadedImage.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(uploadedImage.FileName) +
                               "_" + Guid.NewGuid().ToString() + Path.GetExtension(uploadedImage.FileName);

                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(uploadsFolder, fileName);

                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadedImage.CopyToAsync(stream);
                }

                updatedPhoto.PictureUrl = $"/images/{fileName}";
            }
            else if (string.IsNullOrEmpty(updatedPhoto.PictureUrl))
            {
                var existingPhoto = await this.storageBroker.SelectPhotoByIdAsync(updatedPhoto.Id);
                if (existingPhoto != null)
                {
                    updatedPhoto.PictureUrl = existingPhoto.PictureUrl;
                }
            }

            var existingPhotoToUpdate = await this.storageBroker.SelectPhotoByIdAsync(updatedPhoto.Id);
            if (existingPhotoToUpdate == null)
            {
                return NotFound("Photo not found.");
            }

            existingPhotoToUpdate.NameUz = updatedPhoto.NameUz;
            existingPhotoToUpdate.NameRu = updatedPhoto.NameRu;
            existingPhotoToUpdate.PictureUrl = updatedPhoto.PictureUrl;
            existingPhotoToUpdate.CreateDate = DateTimeOffset.Now;

            try
            {
                await this.storageBroker.UpdatePhotoAsync(existingPhotoToUpdate);
                TempData["Message"] = "Photo successfully updated!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the photo.";
            }

            return RedirectToAction("Photo");
        }

        [HttpPost]
        public async ValueTask<IActionResult> DeletePhoto(int id)
        {
            var newsItem = await this.storageBroker.SelectPhotoByIdAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }

            await this.storageBroker.DeletePhotoAsync(newsItem);

            return RedirectToAction("Photo");
        }

        //ProductOne
        public IActionResult ProOne()
        {
            return View();
        }

        public IActionResult ProTwo()
        {
            return View();
        }
    }
}
