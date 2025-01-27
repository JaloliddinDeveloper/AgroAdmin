// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using AgroAdmin.Brokers.Storages;
using AgroAdmin.Models.Foundations.News;
using AgroAdmin.Models.Foundations.Photos;
using AgroAdmin.Models.Foundations.ProductOnes;
using AgroAdmin.Models.Foundations.ProductTwos;
using AgroAdmin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AgroAdmin.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStorageBroker storageBroker;
        private readonly string uploadsFolder = "/var/www/files";
        private readonly string baseUrl = "http://188.166.190.87";

        public AdminController(
            IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
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

                    yangilik.NewPicture = $"{baseUrl}/{uniqueFileName}";
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

                // Save file to /var/www/files
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

                // Save the file URL to the database
                updatedNew.NewPicture = $"{baseUrl}/{fileName}";
            }
            else if (string.IsNullOrEmpty(updatedNew.NewPicture))
            {
                // Retrieve the existing image if no new image is uploaded
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

            // Update fields
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

                    // Save file to /var/www/files
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

                    // Update the URL to use the base URL
                    photo.PictureUrl = $"{baseUrl}/{uniqueFileName}";
                    photo.CreateDate = DateTimeOffset.Now;
                }

                await this.storageBroker.InsertPhotoAsync(photo);
                TempData["Message"] = "Photo successfully added!";
                return RedirectToAction("Photo");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while adding the photo.";
                // Optionally log the exception for debugging
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
                // Generate a unique file name
                var fileName = Path.GetFileNameWithoutExtension(uploadedImage.FileName) +
                               "_" + Guid.NewGuid().ToString() + Path.GetExtension(uploadedImage.FileName);

                // Save file to /var/www/files
                var filePath = Path.Combine(uploadsFolder, fileName);

                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Save the uploaded file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadedImage.CopyToAsync(stream);
                }

                // Update the PictureUrl with the base URL
                updatedPhoto.PictureUrl = $"{baseUrl}/{fileName}";
            }
            else if (string.IsNullOrEmpty(updatedPhoto.PictureUrl))
            {
                // Retain the existing photo URL if no new image is uploaded
                var existingPhoto = await this.storageBroker.SelectPhotoByIdAsync(updatedPhoto.Id);
                if (existingPhoto != null)
                {
                    updatedPhoto.PictureUrl = existingPhoto.PictureUrl;
                }
            }

            // Retrieve the existing photo from the database
            var existingPhotoToUpdate = await this.storageBroker.SelectPhotoByIdAsync(updatedPhoto.Id);
            if (existingPhotoToUpdate == null)
            {
                return NotFound("Photo not found.");
            }

            // Update the photo details
            existingPhotoToUpdate.NameUz = updatedPhoto.NameUz;
            existingPhotoToUpdate.NameRu = updatedPhoto.NameRu;
            existingPhotoToUpdate.PictureUrl = updatedPhoto.PictureUrl;
            existingPhotoToUpdate.CreateDate = DateTimeOffset.Now;

            try
            {
                // Save changes to the database
                await this.storageBroker.UpdatePhotoAsync(existingPhotoToUpdate);
                TempData["Message"] = "Photo successfully updated!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the photo.";
                // Optionally log the exception for debugging
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
            // Ensure the base directory exists
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Save product picture if provided
            if (productPicture != null && productPicture.Length > 0)
            {
                var productPictureFileName = $"{Guid.NewGuid()}_{productPicture.FileName}";
                var productPicturePath = Path.Combine(uploadsFolder, productPictureFileName);

                // Save the product picture file to the server
                using (var stream = new FileStream(productPicturePath, FileMode.Create))
                {
                    await productPicture.CopyToAsync(stream);
                }

                // Store the fully qualified URL for the product picture (with base URL)
                product.ProductPicture = $"{baseUrl}/files/{productPictureFileName}";
            }

            // Save icon URL if provided
            if (iconUrl != null && iconUrl.Length > 0)
            {
                var iconFileName = $"{Guid.NewGuid()}_{iconUrl.FileName}";
                var iconUrlPath = Path.Combine(uploadsFolder, iconFileName);

                // Save the icon file to the server
                using (var stream = new FileStream(iconUrlPath, FileMode.Create))
                {
                    await iconUrl.CopyToAsync(stream);
                }

                // Store the fully qualified URL for the icon (with base URL)
                product.IconUrl = $"{baseUrl}/files/{iconFileName}";
            }

            // Create a new product instance and populate it with the product data
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

            // Save the new product instance to the database
            await this.storageBroker.InsertProductOneAsync(newProduct);

            // Redirect to the product listing page (ProOne)
            return RedirectToAction("ProOne");
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
                var productPicturePath = Path.Combine(uploadsFolder, productPicture.FileName);
                var productPictureDirectory = Path.GetDirectoryName(productPicturePath);
                if (!Directory.Exists(productPictureDirectory))
                {
                    Directory.CreateDirectory(productPictureDirectory);
                }

                using (var stream = new FileStream(productPicturePath, FileMode.Create))
                {
                    await productPicture.CopyToAsync(stream);
                }

                product.ProductPicture = $"{baseUrl}/files/{productPicture.FileName}";
            }
            else
            {
                product.ProductPicture = existingProduct.ProductPicture;
            }

            if (iconUrl != null && iconUrl.Length > 0)
            {
                var iconUrlPath = Path.Combine(uploadsFolder, iconUrl.FileName);
                var iconUrlDirectory = Path.GetDirectoryName(iconUrlPath);
                if (!Directory.Exists(iconUrlDirectory))
                {
                    Directory.CreateDirectory(iconUrlDirectory);
                }

                using (var stream = new FileStream(iconUrlPath, FileMode.Create))
                {
                    await iconUrl.CopyToAsync(stream);
                }

                product.IconUrl = $"{baseUrl}/files/{iconUrl.FileName}";
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

        //======================================================//
        [HttpGet]
        public async ValueTask<IActionResult> ProTwo()
        {
            var prones = await this.storageBroker.SelectAllProductTwosAsync();
            return View(prones);
        }

        [HttpGet]
        public async ValueTask<IActionResult> AddProductTwo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductTwo(ProductTwo product, IFormFile productPicture, IFormFile iconUrl)
        {
            if (product == null)
            {
                return BadRequest("Product details are required.");
            }

            // Handle ProductPicture upload
            if (productPicture != null && productPicture.Length > 0)
            {
                var productPicturePath = Path.Combine(uploadsFolder, productPicture.FileName);
                var productPictureDirectory = Path.GetDirectoryName(productPicturePath);

                // Ensure the directory exists
                if (!Directory.Exists(productPictureDirectory))
                {
                    Directory.CreateDirectory(productPictureDirectory);
                }

                // Save the file
                using (var stream = new FileStream(productPicturePath, FileMode.Create))
                {
                    await productPicture.CopyToAsync(stream);
                }

                // Set the file path with baseUrl for ProductPicture
                product.ProductPicture = $"{baseUrl}/files/{productPicture.FileName}"; // Full URL path for accessing the file
            }

            // Handle IconUrl upload
            if (iconUrl != null && iconUrl.Length > 0)
            {
                var iconUrlPath = Path.Combine(uploadsFolder, iconUrl.FileName);
                var iconUrlDirectory = Path.GetDirectoryName(iconUrlPath);

                // Ensure the directory exists
                if (!Directory.Exists(iconUrlDirectory))
                {
                    Directory.CreateDirectory(iconUrlDirectory);
                }

                // Save the file
                using (var stream = new FileStream(iconUrlPath, FileMode.Create))
                {
                    await iconUrl.CopyToAsync(stream);
                }

                // Set the file path with baseUrl for IconUrl
                product.ProductIcon = $"{baseUrl}/files/{iconUrl.FileName}"; // Full URL path for accessing the file
            }

            // Create a new ProductTwo object and map properties
            var newProductTwo = new ProductTwo
            {
                TitleUz = product.TitleUz,
                TitleRu = product.TitleRu,
                NameUz = product.NameUz,
                NameRu = product.NameRu,
                DesUz = product.DesUz,
                DesRu = product.DesRu,
                DescriptionUZ = product.DescriptionUZ,
                DescriptionRu = product.DescriptionRu,
                SarfUz = product.SarfUz,
                SarfRu = product.SarfRu,
                ProductPicture = product.ProductPicture,
                ProductIcon = product.ProductIcon,
                ProductTwoType = product.ProductTwoType
            };

            // Save the product to the database
            await this.storageBroker.InsertProductTwoAsync(newProductTwo);

            return RedirectToAction("ProTwo");
        }

        [HttpGet]
        public async ValueTask<IActionResult> UpdateProductTwo(int id)
        {
            var product = await this.storageBroker.SelectProductTwoByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async ValueTask<IActionResult> UpdateProductTwo(int id, ProductTwo product, IFormFile productPicture, IFormFile iconUrl)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var existingProduct = await this.storageBroker.SelectProductTwoByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            if (productPicture != null && productPicture.Length > 0)
            {
                var productPicturePath = Path.Combine(uploadsFolder, productPicture.FileName);
                var productPictureDirectory = Path.GetDirectoryName(productPicturePath);

                if (!Directory.Exists(productPictureDirectory))
                {
                    Directory.CreateDirectory(productPictureDirectory);
                }

                using (var stream = new FileStream(productPicturePath, FileMode.Create))
                {
                    await productPicture.CopyToAsync(stream);
                }

                product.ProductPicture = $"{baseUrl}/files/{productPicture.FileName}";
            }
            else
            {
                product.ProductPicture = existingProduct.ProductPicture;
            }

            if (iconUrl != null && iconUrl.Length > 0)
            {
                var iconUrlPath = Path.Combine(uploadsFolder, iconUrl.FileName);
                var iconUrlDirectory = Path.GetDirectoryName(iconUrlPath);

                if (!Directory.Exists(iconUrlDirectory))
                {
                    Directory.CreateDirectory(iconUrlDirectory);
                }

                using (var stream = new FileStream(iconUrlPath, FileMode.Create))
                {
                    await iconUrl.CopyToAsync(stream);
                }

                product.ProductIcon = $"{baseUrl}/files/{iconUrl.FileName}";
            }
            else
            {
                product.ProductIcon = existingProduct.ProductIcon;
            }

            existingProduct.TitleUz = product.TitleUz;
            existingProduct.TitleRu = product.TitleRu;
            existingProduct.NameUz = product.NameUz;
            existingProduct.NameRu = product.NameRu;
            existingProduct.DesUz = product.DesUz;
            existingProduct.DesRu = product.DesRu;
            existingProduct.DescriptionUZ = product.DescriptionUZ;
            existingProduct.DescriptionRu = product.DescriptionRu;
            existingProduct.SarfUz = product.SarfUz;
            existingProduct.SarfRu = product.SarfRu;
            existingProduct.ProductPicture = product.ProductPicture;
            existingProduct.ProductIcon = product.ProductIcon;
            existingProduct.ProductTwoType = product.ProductTwoType;

            await this.storageBroker.UpdateProductTwoAsync(existingProduct);

            return RedirectToAction("ProTwo");
        }

        [HttpPost]
        public async ValueTask<IActionResult> DeleteProductTwo(int id)
        {
            var newsItem = await this.storageBroker.SelectProductTwoByIdAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }

            await this.storageBroker.DeleteProductTwoAsync(newsItem);

            return RedirectToAction("ProTwo");
        }

        public async Task<IActionResult> JadvalIkki(int id)
        {
            var proTwo = await this.storageBroker.SelectProductTwoByIdAsync(id);

            if (proTwo == null)
            {
                return NotFound();
            }

            var jadvallar = await storageBroker.GetTableTwosProOneByIdAsync(id);

            var viewModel = new TableTwoPageViewModel
            {
                ProuctTwoId = proTwo.Id,
                ProductTwoName = proTwo.TitleUz,
                TableTwoes = jadvallar
            };

            return View(viewModel);
        }

        public async Task<IActionResult> AddJadvalIkki(int id)
        {
            var proTwo = await this.storageBroker.SelectProductTwoByIdAsync(id);

            if (proTwo == null)
            {
                return NotFound();
            }

            var word = new TableTwo
            {
                ProductTwoId = proTwo.Id
            };
            return View(word);
        }

        [HttpPost]
        public async ValueTask<IActionResult> AddJadvalIkki(TableTwo tableTwo)
        {

            var tableOneTrue = new TableTwo
            {
               NameUz = tableTwo.NameUz,
               NameRu= tableTwo.NameRu,
               Foiz= tableTwo.Foiz,
               ProductTwoId= tableTwo.ProductTwoId,
            };

            await this.storageBroker.InsertTableTwoAsync(tableOneTrue);

            return RedirectToAction("JadvalIkki", new { id = tableTwo.ProductTwoId });
        }

        public async Task<IActionResult> EditJadvalIkki(int id)
        {
            var tableTwo = await this.storageBroker.SelectTableTwoByIdAsync(id);

            if (tableTwo == null)
            {
                return NotFound();
            }

            return View(tableTwo);
        }

        [HttpPost]
        public async Task<IActionResult> EditJadvalIkki(int id, TableTwo tableTwo)
        {
            if (id != tableTwo.Id)
            {
                return NotFound();
            }

            var existingTableTwo = await this.storageBroker.SelectTableTwoByIdAsync(id);

            if (existingTableTwo == null)
            {
                return NotFound();
            }

            existingTableTwo.NameUz = tableTwo.NameUz;
            existingTableTwo.NameRu = tableTwo.NameRu;
            existingTableTwo.Foiz = tableTwo.Foiz;
            existingTableTwo.ProductTwoId = tableTwo.ProductTwoId;
          


            await this.storageBroker.UpdateTableTwoAsync(existingTableTwo);

            return RedirectToAction("JadvalIkki", new { id = tableTwo.ProductTwoId });
        }

        [HttpPost]
        public async ValueTask<IActionResult> DeleteIkki(int id)
        {
            var newsItem = await this.storageBroker.SelectTableTwoByIdAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }

            await this.storageBroker.DeleteTableTwoAsync(newsItem);

            return RedirectToAction("JadvalIkki", new { id = newsItem.ProductTwoId });
        }
    }
}
