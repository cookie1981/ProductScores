//using System.Net.Http;
//using System.Web.Http;
//
//namespace api.Controllers
//{
//    public class SwaggerController : ApiController
//    {
//        [HttpGet]
//        public HttpResponseMessage GetSpec()
//        {
//            return new HttpResponseMessage()
//            {
//                Content = new StringContent(
//                    "swagger: \'2.0\'\r\ninfo:\r\n  title: Product Immune System Service\r\n  description: Provides CRUD functionality for a Team/Product Immunisation Scores @ CTM.\r\n  version: \"1.0.0\"\r\n  contact: David Cook\r\n    name: David Cook\r\n    url: \r\n    email: david.cook@bglgroup.co.uk\r\n# the domain of the service\r\nhost: TBD\r\n# array of all schemes that your API supports\r\nschemes:\r\n  - http\r\n# format of bodies a client can send (Content-Type)\r\nconsumes:\r\n  - application/json\r\n# format of the responses to the client (Accepts)\r\nproduces:\r\n  - application/json\r\npaths:\r\n  /api/ProductScores:\r\n    get:\r\n      summary: returns Immune system scores for all products \r\n      description: returns Immune system scores for all products.\r\n      parameters:\r\n        \r\n      security:\r\n        #- bearer:\r\n         # - riskcapture\r\n      responses:\r\n        200:\r\n          description: (Ok) Immune system scores have been found and retrieved from datastore\r\n          headers:\r\n          schema:\r\n    #        $ref: \'#/definitions/ValidationResponse\'\r\n          examples:\r\n             {\r\n\t\t\t\tapplication/json: [{\"Id\": \"590b50f8de13257f88ae8a8b\",\"Product\": [\"travel\"],\"Team\": \"mob\",\"SDET\": \"terry\",\"ImmuneSystem\": {\"Scores\": [{\"Category\": \"automation\",\"Grade\": 3},{\"Category\": \"x-browser\",\"Grade\": 3}]}}]\r\n             }\r\n        500:\r\n          description: (InternalServerError) something went wrong attempting to bring back results\r\n ",
//                    System.Text.Encoding.UTF8,
//                    "text/html"
//                )
//            };
//        }
//    }
//}
