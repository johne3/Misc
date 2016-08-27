using ECommerce.Repository.Abstract;
using ECommerce.Repository.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Routing;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace ECommerce.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory, IDependencyResolver
    {
        private readonly IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext
            requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            ninjectKernel.Bind<ICategoryRepository>().To<EFCategoryRepository>();
            //ninjectKernel.Bind<IUserRepository>().To<EFUserRepository>();
            ninjectKernel.Bind<IFeatureRequestRepository>().To<EFFeatureRequestRepository>();

            var emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            ninjectKernel.Bind<IOrderProcessor>()
                .To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);

            //ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }

        #region IDependencyResolverMembers

        public object GetService(Type serviceType)
        {
            return ninjectKernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object> {ninjectKernel.TryGet(serviceType)};
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            //When BeginScope returns 'this', the Dispose method must be a no-op.
        }


        #endregion
    }
}
