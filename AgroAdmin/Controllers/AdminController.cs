// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using AgroAdmin.Brokers.Storages;
using AgroAdmin.Models.Foundations.News;
using AgroAdmin.Models.Foundations.Photos;
using AgroAdmin.Models.Foundations.ProductOnes;
using AgroAdmin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AgroAdmin.Controllers
{
    public class AdminController : Controller
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

        public ActionResult Index() =>
             View();

        //Yangilik
        [HttpGet]
        public async ValueTask<IActionResult> Yangilik()
        {
            var newss = await this.storageBroker.SelectAllNewsAsync();

            return View(newss);
        }

        [HttpGet]
        public async ValueTask<IActionResult> AddYangilik() =>
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
        public async ValueTask<IActionResult> AddPhoto() =>
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

        [HttpGet]
        public async ValueTask<IActionResult> ProOne()
        {
            var prones = await this.storageBroker.SelectAllProductOnesAsync();
            return View(prones);
        }

        [HttpGet]
        public async ValueTask<IActionResult> AddProductOne()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductOne(ProductOne product, IFormFile productPicture, IFormFile iconUrl)
        {
            if (true)
            {
                if (productPicture != null && productPicture.Length > 0)
                {
                    var productPicturePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", productPicture.FileName);

                    var productPictureDirectory = Path.GetDirectoryName(productPicturePath);
                    if (!Directory.Exists(productPictureDirectory))
                    {
                        Directory.CreateDirectory(productPictureDirectory);
                    }

                    using (var stream = new FileStream(productPicturePath, FileMode.Create))
                    {
                        await productPicture.CopyToAsync(stream);
                    }

                    product.ProductPicture = "/images/" + productPicture.FileName;
                }

                if (iconUrl != null && iconUrl.Length > 0)
                {
                    var iconUrlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", iconUrl.FileName);

                    var iconUrlDirectory = Path.GetDirectoryName(iconUrlPath);
                    if (!Directory.Exists(iconUrlDirectory))
                    {
                        Directory.CreateDirectory(iconUrlDirectory);
                    }

                    using (var stream = new FileStream(iconUrlPath, FileMode.Create))
                    {
                        await iconUrl.CopyToAsync(stream);
                    }

                    product.IconUrl = "/images/" + iconUrl.FileName;
                }
                var newProduct = new ProductOne
                {
                    TitleUz = product.TitleUz,
                    TitleRu = product.TitleRu,
                    DesUz = product.DesUz,
                    DesRu = product.DesRu,
                    DescriptionUz = product.DescriptionUz,
                    DescriptionRu = product.DescriptionRu,
                    TasirModdaUz = product.TasirModdaUz,
                    TasirModdaRu = product.TasirModdaRu,
                    KimyoviySinfiUz = product.KimyoviySinfiUz,
                    KimyoviySinfiRu = product.KimyoviySinfiRu,
                    PreparatShakliUz = product.PreparatShakliUz,
                    PreparatShakliRu = product.PreparatShakliRu,
                    QadogiUz = product.QadogiUz,
                    QadogiRu = product.QadogiRu,
                    IconUrl = product.IconUrl,
                    ProductPicture = product.ProductPicture,
                    ProductType = product.ProductType,
                    AdditionUz = product.AdditionUz,
                    AdditionRu = product.AdditionRu
                };
                await this.storageBroker.InsertProductOneAsync(newProduct);

                return RedirectToAction("ProOne");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProductOne(int id)
        {
            var product = await this.storageBroker.SelectProductOneByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductOne(int id, ProductOne product, IFormFile productPicture, IFormFile iconUrl)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var existingProduct = await this.storageBroker.SelectProductOneByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            if (productPicture != null && productPicture.Length > 0)
            {
                var productPicturePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", productPicture.FileName);
                var productPictureDirectory = Path.GetDirectoryName(productPicturePath);
                if (!Directory.Exists(productPictureDirectory))
                {
                    Directory.CreateDirectory(productPictureDirectory);
                }

                using (var stream = new FileStream(productPicturePath, FileMode.Create))
                {
                    await productPicture.CopyToAsync(stream);
                }

                product.ProductPicture = "/images/" + productPicture.FileName;
            }
            else
            {
                product.ProductPicture = existingProduct.ProductPicture;
            }

            if (iconUrl != null && iconUrl.Length > 0)
            {
                var iconUrlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", iconUrl.FileName);
                var iconUrlDirectory = Path.GetDirectoryName(iconUrlPath);
                if (!Directory.Exists(iconUrlDirectory))
                {
                    Directory.CreateDirectory(iconUrlDirectory);
                }

                using (var stream = new FileStream(iconUrlPath, FileMode.Create))
                {
                    await iconUrl.CopyToAsync(stream);
                }

                product.IconUrl = "/images/" + iconUrl.FileName;
            }
            else
            {
                product.IconUrl = existingProduct.IconUrl;
            }

            existingProduct.TitleUz = product.TitleUz;
            existingProduct.TitleRu = product.TitleRu;
            existingProduct.DesUz = product.DesUz;
            existingProduct.DesRu = product.DesRu;
            existingProduct.DescriptionUz = product.DescriptionUz;
            existingProduct.DescriptionRu = product.DescriptionRu;
            existingProduct.TasirModdaUz = product.TasirModdaUz;
            existingProduct.TasirModdaRu = product.TasirModdaRu;
            existingProduct.KimyoviySinfiUz = product.KimyoviySinfiUz;
            existingProduct.KimyoviySinfiRu = product.KimyoviySinfiRu;
            existingProduct.PreparatShakliUz = product.PreparatShakliUz;
            existingProduct.PreparatShakliRu = product.PreparatShakliRu;
            existingProduct.QadogiUz = product.QadogiUz;
            existingProduct.QadogiRu = product.QadogiRu;
            existingProduct.ProductPicture = product.ProductPicture;
            existingProduct.IconUrl = product.IconUrl;
            existingProduct.ProductType = product.ProductType;
            existingProduct.AdditionUz = product.AdditionUz;
            existingProduct.AdditionRu = product.AdditionRu;

            await this.storageBroker.UpdateProductOneAsync(existingProduct);

            return RedirectToAction("ProOne");
        }


        [HttpPost]
        public async ValueTask<IActionResult> DeleteProductOne(int id)
        {
            var newsItem = await this.storageBroker.SelectProductOneByIdAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }

            await this.storageBroker.DeleteProductOneAsync(newsItem);

            return RedirectToAction("ProOne");

        }


        public async Task<IActionResult> JadvalBir(int id)
        {
            var proOne = await this.storageBroker.SelectProductOneByIdAsync(id);

            if (proOne == null)
            {
                return NotFound();
            }

            var jadvallar = await storageBroker.GetTableOnesProOneByIdAsync(id);

            var viewModel = new TableOnePageViewModel
            {
                ProuctOneId = proOne.Id,
                ProductOnename = proOne.TitleUz,
                TableOnes = jadvallar
            };

            return View(viewModel);
        }

        public async Task<IActionResult> AddJadvalBir(int id)
        {
            var proOne = await this.storageBroker.SelectProductOneByIdAsync(id);

            if (proOne == null)
            {
                return NotFound();
            }

            var word = new TableOne
            {
                ProductOneId = proOne.Id
            };
            return View(word);
        }

        [HttpPost]
        public async ValueTask<IActionResult> AddJadvalBir(TableOne tableOne)
        {

            var tableOneTrue = new TableOne
            {
                EkinTuriUz = tableOne.EkinTuriUz,
                EkinTuriRu = tableOne.EkinTuriRu,
                BegonaQarshiUz = tableOne.BegonaQarshiUz,
                BegonaQarshiRu = tableOne.BegonaQarshiRu,
                SarfMeyoriUz = tableOne.SarfMeyoriUz,
                SarfMeyoriRu = tableOne.SarfMeyoriRu,
                BirgaSarfUz = tableOne.BirgaSarfUz,
                BirgaSarfRu = tableOne.BirgaSarfRu,
                Onlsum = tableOne.Onlsum,
                ProductOneId = tableOne.ProductOneId
            };

            // Don't set Id here, let SQL Server handle it.
            await this.storageBroker.InsertTableOneAsync(tableOneTrue);

            return RedirectToAction("JadvalBir", new { id = tableOne.ProductOneId });

        }

        public async Task<IActionResult> EditJadvalBir(int id)
        {
            var tableOne = await this.storageBroker.SelectTableOneByIdAsync(id);

            if (tableOne == null)
            {
                return NotFound();
            }

            return View(tableOne);
        }

        [HttpPost]
        public async Task<IActionResult> EditJadvalBir(int id, TableOne tableOne)
        {
            if (id != tableOne.Id)
            {
                return NotFound();
            }

            var existingTableOne = await this.storageBroker.SelectTableOneByIdAsync(id);

            if (existingTableOne == null)
            {
                return NotFound();
            }

            existingTableOne.EkinTuriUz = tableOne.EkinTuriUz;
            existingTableOne.EkinTuriRu = tableOne.EkinTuriRu;
            existingTableOne.BegonaQarshiUz = tableOne.BegonaQarshiUz;
            existingTableOne.BegonaQarshiRu = tableOne.BegonaQarshiRu;
            existingTableOne.SarfMeyoriUz = tableOne.SarfMeyoriUz;
            existingTableOne.SarfMeyoriRu = tableOne.SarfMeyoriRu;
            existingTableOne.BirgaSarfUz = tableOne.BirgaSarfUz;
            existingTableOne.BirgaSarfRu = tableOne.BirgaSarfRu;
            existingTableOne.Onlsum = tableOne.Onlsum;
            existingTableOne.ProductOneId = tableOne.ProductOneId;

            await this.storageBroker.UpdateTableOneAsync(existingTableOne);

            return RedirectToAction("JadvalBir", new { id = tableOne.ProductOneId });
        }


        [HttpPost]
        public async ValueTask<IActionResult> DeleteJadvalBir(int id)
        {
            var newsItem = await this.storageBroker.SelectTableOneByIdAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }

            await this.storageBroker.DeleteTableOneAsync(newsItem);

            return RedirectToAction("JadvalBir", new { id = newsItem.ProductOneId });

        }
        public IActionResult ProTwo()
        {
            return View();
        }
    }
}
