//using ECommerce.Domain.Entities;
//using ECommerce.Repository.Abstract;
//using ECommerce.WebUI.Controllers;
//using ECommerce.WebUI.HtmlHelpers;
//using ECommerce.WebUI.Models;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;

//namespace ECommerce.UnitTests
//{
//    [TestClass]
//    public class ProductCatalogTests
//    {
//        [TestMethod]
//        public void Can_Paginate()
//        {
//            //Arrange
//            var mock = new Mock<IProductRepository>();

//            mock.Setup(m => m.Products).Returns(new[]
//                {
//                    new Product {ProductId = 1, Name = "P1"},
//                    new Product {ProductId = 2, Name = "P2"},
//                    new Product {ProductId = 3, Name = "P3"},
//                    new Product {ProductId = 4, Name = "P4"},
//                    new Product {ProductId = 5, Name = "P5"}
//                }.AsQueryable());

//            var controller = new ProductController(mock.Object) {PageSize = 3};

//            //Act
//            var result = (ProductsListViewModel) controller.List(null, 2).Model;

//            //Assert
//            var productArray = result.Products.ToArray();
//            Assert.IsTrue(productArray.Length == 2);
//            Assert.AreEqual(productArray[0].Name, "P4");
//            Assert.AreEqual(productArray[1].Name, "P5");
//        }

//        [TestMethod]
//        public void Can_Generate_Page_Links()
//        {
//            //Arrange - define an HTML helper - we need to do this
//            //in order to apply the extension method
//            HtmlHelper myHelper = null;

//            //Arrange - create PagingInfo data
//            var pagingInfo = new PagingInfo
//                {
//                    CurrentPage = 2,
//                    TotalItems = 28,
//                    ItemsPerPage = 10
//                };

//            //Arrange - set up the delegate using a lambda expression
//            Func<int, string> pageUrlDelegate = i => "Page" + i;

//            //Act
//            var result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

//            //Assert
//            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>"
//                                               + @"<a class=""selected"" href=""Page2"">2</a>"
//                                               + @"<a href=""Page3"">3</a>");
//        }

//        [TestMethod]
//        public void Can_Send_Pagination_View_Model()
//        {
//            //Arrange
//            var mock = new Mock<IProductRepository>();
//            mock.Setup(m => m.Products).Returns(new[]
//                {
//                    new Product {ProductId = 1, Name = "P1"},
//                    new Product {ProductId = 2, Name = "P2"},
//                    new Product {ProductId = 3, Name = "P3"},
//                    new Product {ProductId = 4, Name = "P4"},
//                    new Product {ProductId = 5, Name = "P5"}
//                }.AsQueryable());

//            //Arrange
//            var controller = new ProductController(mock.Object);
//            controller.PageSize = 3;

//            //Act
//            var result = (ProductsListViewModel) controller.List(null, 2).Model;

//            //Assert
//            var pageInfo = result.PagingInfo;
//            Assert.AreEqual(pageInfo.CurrentPage, 2);
//            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
//            Assert.AreEqual(pageInfo.TotalItems, 5);
//            Assert.AreEqual(pageInfo.TotalPages, 2);
//        }

//        [TestMethod]
//        public void Can_Filter_Products()
//        {
//            //Arrange - create the mock repository
//            var mock = new Mock<IProductRepository>();
//            mock.Setup(m => m.Products).Returns(new Product[]
//                {
//                    new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
//                    new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
//                    new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
//                    new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
//                    new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
//                }.AsQueryable());

//            //Arrange - create a controller and make the page size 3 items
//            var controller = new ProductController(mock.Object);
//            controller.PageSize = 3;

//            //Action
//            var result = ((ProductsListViewModel) controller.List("Cat2", 1).Model).Products.ToArray();

//            //Assert
//            Assert.AreEqual(result.Length, 2);
//            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
//            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
//        }

//        [TestMethod]
//        public void Can_Create_Categories()
//        {
//            //Arrange - create the mock object
//            var mock = new Mock<IProductRepository>();
//            mock.Setup(m => m.Products).Returns(new Product[]
//                {
//                    new Product {ProductId = 1, Name = "P1", Category = "Apples"},
//                    new Product {ProductId = 2, Name = "P2", Category = "Apples"},
//                    new Product {ProductId = 3, Name = "P3", Category = "Plums"},
//                    new Product {ProductId = 4, Name = "P4", Category = "Oranges"}
//                }.AsQueryable());

//            //Arrange - create the controller
//            var target = new NavController(mock.Object);

//            //Act = get the set of categories
//            var results = ((IEnumerable<string>) target.Menu().Model).ToArray();

//            //Assert
//            Assert.AreEqual(results.Length, 3);
//            Assert.AreEqual(results[0], "Apples");
//            Assert.AreEqual(results[1], "Oranges");
//            Assert.AreEqual(results[2], "Plums");
//        }

//        [TestMethod]
//        public void Indicates_Selected_Category()
//        {
//            //Arrange - create the mock repository
//            var mock = new Mock<IProductRepository>();
//            mock.Setup(m => m.Products).Returns(new Product[]
//                {
//                    new Product {ProductId = 1, Name = "P1", Category = "Apples"},
//                    new Product {ProductId = 2, Name = "P2", Category = "Oranges"}
//                }.AsQueryable());

//            //Arrange - create the controller
//            var target = new NavController(mock.Object);

//            //Arrange - define the category to select
//            string categoryToSelect = "Apples";

//            //Action
//            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

//            //Assert
//            Assert.AreEqual(categoryToSelect, result);
//        }

//        [TestMethod]
//        public void Generate_Category_Specific_Product_Count()
//        {
//            //Arrange - crate the mock repository
//            var mock = new Mock<IProductRepository>();
//            mock.Setup(m => m.Products).Returns(new Product[]
//                {
//                    new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
//                    new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
//                    new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
//                    new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
//                    new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
//                }.AsQueryable());

//            //Arrange - create a controller and make the page size 3 items
//            var target = new ProductController(mock.Object);
//            target.PageSize = 3;

//            //Action - test the product coutns for differenct categories
//            int res1 = ((ProductsListViewModel) target.List("Cat1").Model).PagingInfo.TotalItems;
//            int res2 = ((ProductsListViewModel) target.List("Cat2").Model).PagingInfo.TotalItems;
//            int res3 = ((ProductsListViewModel) target.List("Cat3").Model).PagingInfo.TotalItems;
//            int resAll = ((ProductsListViewModel) target.List(null).Model).PagingInfo.TotalItems;

//            //Assert
//            Assert.AreEqual(res1, 2);
//            Assert.AreEqual(res2, 2);
//            Assert.AreEqual(res3, 1);
//            Assert.AreEqual(resAll, 5);
//        }
//    }
//}
