//using Autofac;
//using System.Web.Http;

//namespace ProductImmuneSystemScores.App_Start
//{
//    public class AutoFacConfig
//    {
//        private static IContainer Container { get; set; }

//        public void Initialise(HttpConfiguration config)
//        {
//            var builder = new ContainerBuilder();

//            config.DependencyResolver = new AutofacWebMvcDependencyResolver(Container);

//            builder.RegisterType<ApiClient.ProductScoreApiSection>()
//                .As<ApiClient.IProductScoresApiConfig>()
//                .InstancePerRequest();


//            Container = builder.Build();
//        }
//    }
//}