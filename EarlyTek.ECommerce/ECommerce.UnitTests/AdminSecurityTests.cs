//using System;
//using System.Web.Mvc;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using ECommerce.WebUI.Controllers;
//using ECommerce.WebUI.Infrastructure.Abstract;
//using ECommerce.WebUI.Models;

//namespace ECommerce.UnitTests
//{
//    [TestClass]
//    public class AdminSecurityTests
//    {
//        [TestMethod]
//        public void Can_Login_With_Valid_Credentials()
//        {
//            //Arrange - create a mock authentication provider
//            var mock = new Mock<IAuthProvider>();
//            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

//            //Arrange - create the view model
//            var model = new LoginViewModel
//                {
//                    Username = "admin",
//                    Password = "secret"
//                };

//            //Arrange - create the controller
//            var target = new AccountController(mock.Object);

//            //Act - authenticate using valid credentials
//            var result = target.Login(model, "/MyURL");

//            //Assert
//            Assert.IsInstanceOfType(result, typeof (RedirectResult));
//            Assert.AreEqual("/MyURL", ((RedirectResult) result).Url);
//        }
        
//        [TestMethod]
//        public void Cannot_Login_With_Invalid_Credentials()
//        {
//            //Arrange - create a mock authentication provider
//            var mock = new Mock<IAuthProvider>();
//            mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);

//            //Arrange - create the view model
//            var model = new LoginViewModel
//                {
//                    Username = "badUser",
//                    Password = "badPass"
//                };

//            //Arrange - create the controller
//            var target = new AccountController(mock.Object);

//            //Act - authenticate using valid credentials
//            var result = target.Login(model, "/MyURL");

//            //Assert
//            Assert.IsInstanceOfType(result, typeof (ViewResult));
//            Assert.IsFalse(((ViewResult) result).ViewData.ModelState.IsValid);
//        }
//    }
//}
