using Microsoft.Data.Edm;
using Newtonsoft.Json.Serialization;
using ProductsCatalog.Models;
using ProductsCatalog.WebApi.Models;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;

namespace ProductsCatalog.WebApi
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {

            config.Routes.MapODataServiceRoute("odata", "odata", GetImplicitEDM());
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Returns CamelCase
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Add support CORS
            var attr = new EnableCorsAttribute("*", "*", "*", "DataServiceVersion, MaxDataServiceVersion");
            config.EnableCors(attr);

        }

        private static IEdmModel GetImplicitEDM()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<CategoryViewModel>("Categories");
           var products = builder.EntitySet<ProductViewModel>("Products");

            return builder.GetEdmModel(); // magic happens here
        }
    }
}
