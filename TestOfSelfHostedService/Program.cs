using System;
using Microsoft.Owin.Hosting;
using Owin;

namespace TestOfSelfHostedService
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://localhost:50504/*";

            using (WebApp.Start<StartUp>(url))
            {
                Console.ReadKey();
            }
        }
    }

    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            app.Run(x => x.Response.WriteAsync("{\r\n    \"_id\" : ObjectId(\"591189a4de132559c853dbb6\"),\r\n    \"product\" : \"car\",\r\n    \"team\" : \"Motor\",\r\n    \"SDET\" : \"smattock\",\r\n    \"immuneSystem\" : {\r\n        \"scores\" : [ \r\n            {\r\n                \"category\" : \"automation\",\r\n                \"grade\" : 3\r\n            }, \r\n            {\r\n                \"category\" : \"x-browser\",\r\n                \"grade\" : 3\r\n            }, \r\n            {\r\n                \"category\" : \"monitoring-and-alerting\",\r\n                \"grade\" : 3\r\n            }, \r\n            {\r\n                \"category\" : \"release\",\r\n                \"grade\" : 3\r\n            }, \r\n            {\r\n                \"category\" : \"tech-debt\",\r\n                \"grade\" : 3\r\n            }, \r\n            {\r\n                \"category\" : \"security\",\r\n                \"grade\" : 3\r\n            }, \r\n            {\r\n                \"category\" : \"performance\",\r\n                \"grade\" : 3\r\n            }\r\n        ]\r\n    }\r\n}"));
        }
    }
}
