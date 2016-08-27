using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using ECommerce.Domain.Entities;

namespace ECommerce.WebUI.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string SessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //Get the Cart from the session
            var cart = (Cart) controllerContext.HttpContext.Session[SessionKey];
            //create the Cart if there wasn't one in the session data
            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[SessionKey] = cart;
            }
        
            //return the cart
            return cart;
        }
    }
}