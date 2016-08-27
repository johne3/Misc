using ECommerce.Domain.Entities;
using ECommerce.WebUI.Binders;
using ECommerce.WebUI.Infrastructure;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ECommerce.WebUI
{ 
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //Sets the controller factory to build controllers
            var ninjectControllerFactory = new NinjectControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(ninjectControllerFactory);
            GlobalConfiguration.Configuration.DependencyResolver = ninjectControllerFactory;

            //Tells the MVC framework that it can use our CartModelBinder class
            //to create instances of Cart
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}