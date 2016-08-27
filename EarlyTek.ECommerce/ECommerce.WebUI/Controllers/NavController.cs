using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Domain.Entities;
using ECommerce.Repository.Abstract;

namespace ECommerce.WebUI.Controllers
{
    public class NavController : Controller
    {
        private readonly ICategoryRepository repository;

        public NavController(ICategoryRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(Category category = null)
        {
            ViewBag.SelectedCategory = category;

            //IEnumerable<string> categories = repository.Products
            //                                           .Select(x => x.Category)
            //                                           .Distinct()
            //                                           .OrderBy(x => x);

            var categories = repository.Categories.OrderBy(x => x.Name).ToList();

            return PartialView(categories);
        }

    }
}
 