using System.Web.Http;
using WebActivatorEx;
using ResourceServer.Api;
using Swashbuckle.Application;
using System;
using System.Xml.XPath;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ResourceServer.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            Swashbuckle.Bootstrapper.Init(GlobalConfiguration.Configuration);
            SwaggerSpecConfig.Customize(c =>
            {
                c.IncludeXmlComments(GetXMLComments());
            });
            //var thisAssembly = typeof(SwaggerConfig).Assembly;

            // GlobalConfiguration.Configuration 
            //     .EnableSwagger(c =>
            //         {
            //             c.SingleApiVersion("v1", "ResourceServer.Api");
            //             c.IncludeXmlComments(GetXMLComments());

            //         })
            //     .EnableSwaggerUi();
        }

        private static string GetXMLComments()
        {
            try
            {
                return System.String.Format(@"{0}\bin\ResourceServer.Api.XML", System.AppDomain.CurrentDomain.BaseDirectory);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
