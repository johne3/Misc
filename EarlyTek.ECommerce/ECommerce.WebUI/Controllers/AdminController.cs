using System;
using System.IO;
using System.Web.Security;
using ECommerce.Domain.Entities;
using ECommerce.Repository.Abstract;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.WebUI.Models;

namespace ECommerce.WebUI.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IFeatureRequestRepository featureRequestRepository;

        public AdminController(IProductRepository productRepo, ICategoryRepository categoryRepo, IFeatureRequestRepository featureRequestRepository)
        {
            productRepository = productRepo;
            categoryRepository = categoryRepo;
            this.featureRequestRepository = featureRequestRepository;
        }

        //
        // GET: /Admin/
        public ViewResult Index()
        {
            var featureRequests =
                featureRequestRepository.FeatureRequests.OrderBy(x => x.Status).ThenBy(x => x.CreateDate).ToList();
            return View(featureRequests);
        }

        //
        //GET: /Admin/Categories

        public ViewResult Categories()
        {
            return View(categoryRepository.Categories);
        }

        //
        //GET: /Admin/CreateCategory

        public ViewResult CreateCategory()
        {
            return View("EditCategory", new Category());
        }

        //
        //POST: /Admin/SaveCategory

        [HttpPost]
        public ActionResult SaveCategory(Category category)
        {
            categoryRepository.SaveCategory(category);
            TempData["message"] = string.Format("{0} was saved successfully", category.Name);
            return RedirectToAction("Categories");
        }

        //
        //GET: /Admin/EditCategory

        public ActionResult EditCategory(int categoryId)
        {
            var category = categoryRepository.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            return View(category);
        }

        //
        //GET: /Admin/ConfirmDeleteCategory

        public ActionResult ConfirmDeleteCategory(int id)
        {
            var category = categoryRepository.Categories.FirstOrDefault(c => c.CategoryId == id);
            return View(category);
        }

        //
        //POST: /Admin/DeleteCategory

        [HttpPost]
        public ActionResult DeleteCategory(int categoryId)
        {
            var deletedCategory = categoryRepository.DeleteCategory(categoryId);
            if (deletedCategory != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedCategory.Name);
            }
            return RedirectToAction("Categories");
        }

        //
        //GET: /Admin/Products

        public ViewResult Products(int id)
        {
            var category = categoryRepository.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (category != null)
            {
                category.Products = productRepository.Products.Where(x => x.CategoryId == id).ToList();
            }

            //var products = productRepository.Products.Where(x => x.CategoryId == id);
            return View(category);
        }

        //
        //GET: /Admin/EditProduct

        public ViewResult EditProduct(int productId)
        {
            ViewBag.Categories = categoryRepository.Categories;
            var product = productRepository.Products
                                    .FirstOrDefault(p => p.ProductId == productId);

            return View(product);
        }

        //
        //GET: /Admin/ProductImages

        public ActionResult ProductImages(int productId)
        {
            var product = productRepository.Products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                product.Pictures = productRepository.Pictures.Where(x => x.ProductId == productId).ToList();
            }
            return View(product);
        }

        //
        //GET: /Admin/ProductVideos

        public ActionResult ProductVideos(int productId)
        {
            var product = productRepository.Products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                product.Videos = productRepository.Videos.Where(x => x.ProductId == productId).ToList();
            }

            return View(product);
        }

        //
        //POST: /Admin/SaveVideo

        public ActionResult SaveVideo(SaveVideoModel model)
        {
            var product = productRepository.Products.FirstOrDefault(x => x.ProductId == model.ProductId);
            if (product != null)
            {
                var video = new Video
                {
                    ProductId = model.ProductId,
                    Source = model.Source
                };
                productRepository.Save(video);
                product.Videos = productRepository.Videos.Where(x => x.ProductId == model.ProductId).ToList();
            }

            return View("ProductVideos", product);
        }

        //
        //POST: /Admin/SaveImage

        public ActionResult SaveImage(int productId, HttpPostedFileBase image)
        {
            var product = productRepository.Products.FirstOrDefault(x => x.ProductId == productId);
            if (image != null)
            {
                const string virtualPath = "~/Content/Uploads";
                //Creates a new guid for a unique file name
                var guid = Guid.NewGuid();
                var fileExtension = Path.GetExtension(image.FileName);
                var fileName = string.Format("{0}{1}", guid, fileExtension);

                //Store the file
                var path = Path.Combine(Server.MapPath(virtualPath), fileName);
                image.SaveAs(path);

                var picture = new Picture
                {
                    ProductId = productId,
                    ImagePath = string.Format("{0}/{1}", virtualPath, fileName)
                };
                productRepository.Save(picture);
            }

            if (product != null)
            {
                product.Pictures = productRepository.Pictures.Where(x => x.ProductId == productId).ToList();
            }

            return View("ProductImages", product);
        }

        //
        //POST: /Admin/SaveProduct

        [HttpPost]
        public ActionResult SaveProduct(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    const string virtualPath = "~/Content/Uploads";
                    var oldFileName = string.IsNullOrEmpty(product.Thumbnail.ImagePath)
                        ? null
                        : product.Thumbnail.ImagePath;

                    //Creates a new guid for a unique file name
                    var guid = Guid.NewGuid();
                    var fileExtension = Path.GetExtension(image.FileName);
                    var fileName = string.Format("{0}{1}", guid, fileExtension);

                    //Store the file inside ~/Content/Uploads folder
                    var path = Path.Combine(Server.MapPath(virtualPath), fileName);
                    image.SaveAs(path);
                    product.Thumbnail.ImagePath = string.Format("{0}/{1}", virtualPath, fileName);

                    //Deletes the old image to clean up
                    if (oldFileName != null && System.IO.File.Exists(Server.MapPath(oldFileName)))
                    {
                        System.IO.File.Delete(Server.MapPath(oldFileName));
                    }

                    //product.ImageMimeType = image.ContentType;
                    //product.ImageData = new byte[image.ContentLength];
                    //image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

                productRepository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Products", new {id = product.CategoryId});
            }

            ViewBag.Categories = categoryRepository.Categories;
            //There is something wrong with the data values
            return View("EditProduct", product);
        }

        //
        //GET: /Admin/CreateProduct

        public ViewResult CreateProduct()
        {
            ViewBag.Categories = categoryRepository.Categories;
            var newProduct = new Product
            {
                Thumbnail = new Thumbnail()
            };
            return View("EditProduct", newProduct);
        }

        //
        //GET: /Admin/ConfirmDeleteProduct

        public ActionResult ConfirmDeleteProduct(int productId)
        {
            var product = productRepository.Products.FirstOrDefault(x => x.ProductId == productId);
            return View(product);
        }

        //
        //POST: /Admin/DeleteProduct

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            var deletedProduct = productRepository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
            }
            return RedirectToAction("Products");
        }

        public ActionResult CreateFeatureRequest()
        {
            var newFeatureRequest = new FeatureRequest
            {
                RequestedBy = User.Identity.Name
            };
            return View("EditFeatureRequest", newFeatureRequest);
        }

        public ViewResult EditFeatureRequest(int id)
        {
            var featureRequest = featureRequestRepository.FeatureRequests.Single(x => x.Id == id);
            return View(featureRequest);
        }

        [HttpPost]
        public ActionResult SaveFeatureRequest(FeatureRequest featureRequest)
        {
            featureRequestRepository.Save(featureRequest);
            TempData["message"] = "The feature request was saved successfully";
            return View("Index",
                featureRequestRepository.FeatureRequests.OrderBy(x => x.Status).ThenBy(x => x.CreateDate));
        }

        [HttpPost]
        public ActionResult DeleteFeatureRequest(int id)
        {
            featureRequestRepository.DeleteFeatureRequest(id);
            TempData["message"] = "The feature request was deleted";
            return View("Index",
                featureRequestRepository.FeatureRequests.OrderBy(x => x.Status).ThenBy(x => x.CreateDate));
        }
    }
}
