using System.Web.Http;
using ProductsWebAPI.Service;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;


namespace ProductsWebAPI.App_Start
{
    public static class DependencyInjectionSetup
    {

        public static void Configure()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();


            // Register your dependencies
            container.Register<IProductsService, ProductsService>(Lifestyle.Scoped);
            // Register other services...

            // Register Web API Controllers
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            // Set the dependency resolver for Web API
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            container.Verify();
        }

    }
}