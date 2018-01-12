using System.Web.Http;
using StrategyCorps.CodeSample.WebApi;
using Swashbuckle.Application;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace StrategyCorps.CodeSample.WebApi
{
    /// <summary>
    /// Swagger Config
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Register Swagger Config
        /// </summary>
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "Strategy Corps Code Sample Test");
                        c.IncludeXmlComments($@"{System.AppDomain.CurrentDomain.BaseDirectory}\bin\StrategyCorps.CodeSample.WebApi.XML"); 
                        c.DescribeAllEnumsAsStrings();
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DisableValidator();
                        c.DocExpansion(DocExpansion.List);
                    });
        }
    }
}
