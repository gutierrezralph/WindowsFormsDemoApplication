using System.Web.Http;
using FluentValidation.WebApi;
using Task.Web.Api.Infrastructure.Filter;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using Task.Web.Api.Infrastructure.Handler;
using Ninject;
using Task.BusinessLayer.Interface;
using Task.BusinessLayer.Implementation;

namespace Task.Web.Api
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services
            config.Filters.Add(new ValidateModelStateFilter());
            config.MessageHandlers.Add(new ResponseWrappingHandler());

            //GZip compression
            GlobalConfiguration.Configuration.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            //Initialeze model valition provider
            FluentValidationModelValidatorProvider.Configure(config);

            /// Initialize Kernel
            InitializedKernelForNinject();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        /// <summary>
        /// Initialize Kernel
        /// </summary>
        private static void InitializedKernelForNinject()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IEmployeeBusinessLayer>().To<EmployeeBusinessLayer>();
        }
    }
}
