using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_OnBeginRequest()
        {
            var res = HttpContext.Current.Response;
            var req = HttpContext.Current.Request;
            res.AppendHeader("Access-Control-Allow-Origin", "http://localhost:8081");
            res.AppendHeader("Access-Control-Allow-Credentials", "true");
            res.AppendHeader("Access-Control-Allow-Headers", "Authorization, Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version");
            res.AppendHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE,OPTIONS");
            res.Headers.Remove("Server");

            if (req.HttpMethod == "OPTIONS")
            {
                res.StatusCode = 200;
                res.End();
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            AutofacConfig.Initialize(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}