using ECommerce.Repository.Abstract;
using ECommerce.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        public int PageSize = 6;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public ViewResult List(string categoryName, int page = 1)
        {
            var currentCategory = categoryRepository.Categories.SingleOrDefault(c => c.Name == categoryName);

            var model = new ProductsListViewModel
            {
                Products = productRepository.Products
                                      .Where(p => categoryName == null || p.Category.Name == categoryName)
                                      .OrderBy(p => p.ProductId)
                                      .Skip((page - 1) * PageSize)
                                      .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = categoryName == null ?
                    productRepository.Products.Count() :
                    productRepository.Products.Count(e => e.Category.Name == categoryName)
                },
                CurrentCategory = currentCategory
            };

            return View(model);

            //return View(_repository.Products
            //                       .OrderBy(p => p.ProductId)
            //                       .Skip((page - 1)*PageSize)
            //                       .Take(PageSize));
        }

        public ViewResult Details(int id)
        {
            var prod = productRepository.Products.Single(p => p.ProductId == id);
            if (prod != null)
            {
                prod.Pictures = productRepository.Pictures.Where(x => x.ProductId == prod.ProductId).ToList();
            }
            return View(prod);
        }

        //public FileContentResult GetImage(int productId)
        //{
        //    var prod = productRepository.Products.FirstOrDefault(p => p.ProductId == productId);
        //    if (prod != null)
        //    {
        //        return File(prod.ImageData, prod.ImageMimeType);
        //    }
        //    return null;
        //}
    }
}
