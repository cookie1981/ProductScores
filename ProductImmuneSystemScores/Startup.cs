using Autofac;
using Autofac.Integration.Mvc;
using Owin;
using System.Web.Http;
using System.Web.Mvc;
using System.Configuration;

using ProductImmuneSystemScores.ApiClient;

//[assembly: OwinStartupAttribute(typeof(ProductImmuneSystemScores.Startup))]
namespace ProductImmuneSystemScores
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //AreaRegistration.RegisterAllAreas();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            // In OWIN you create your own HttpConfiguration rather than re-using the GlobalConfiguration.
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute("DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);


            var configInstance = (ProductScoreApiSection)ConfigurationManager.GetSection("ProductScoreApiGroup/ProductScoreApi");

            builder.RegisterInstance(configInstance).As<IProductScoresApiConfig>().SingleInstance().ExternallyOwned();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}