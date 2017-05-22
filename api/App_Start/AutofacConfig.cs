using System.Reflection;
using System.Web.Http;
using api.DatabaseProviders;
using api.Models;
using Autofac;
using Autofac.Integration.WebApi;

namespace api
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        private static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<MongoDatabaseRepository<ProductScore>>()
                .As<IDatabaseRepository<ProductScore>>()
                .WithParameter("collectionName", "ProductScores")
                .InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}