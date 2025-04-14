using System.Web.Http;
using ProductsApp.App_Start;

namespace ProductsApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            DependencyInjectionSetup.Configure();
        }
    }
}
