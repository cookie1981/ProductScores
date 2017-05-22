using System.Net.Http.Headers;
using System.Web.Http;
//using System.Web.Http.Cors;
using Microsoft.Owin.Security.OAuth;

namespace api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            //            var corsAttribute = new EnableCorsAttribute("http://localhost:8081/", "*",
            //                "GET, PUT, PUSH, DELETE, OPTIONS") {SupportsCredentials = true, Headers = { "Access-Control-Allow-Origin"}};
            //            config.EnableCors(corsAttribute);
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
