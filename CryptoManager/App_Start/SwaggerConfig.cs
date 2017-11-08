using System.Web.Http;
using WebActivatorEx;
using CryptoManager;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace CryptoManager
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "CryptoManager");
                    })
                .EnableSwaggerUi(c =>
                    {
                    });
        }
    }
}
