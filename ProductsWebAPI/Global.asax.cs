using System.Web.Http;
using ProductsWebAPI.App_Start;

namespace ProductsWebAPI
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
