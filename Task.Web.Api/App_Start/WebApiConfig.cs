using System.Web.Http;
using FluentValidation.WebApi;
using Task.Web.Api.Infrastructure.Filter;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using Task.Web.Api.Infrastructure.Handler;
using Task.BusinessLayer.Interface;
using Task.BusinessLayer.Implementation;
using Task.Implementation;
using Task.Core.Repositories;
using Task.Implementation.Repository;
using Unity;
using Unity.Lifetime;
using Task.Web.Api.Resolver;

namespace Task.Web.Api
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services
            InitializeHandlers(config);

            //Initialeze model valition provider
            FluentValidationModelValidatorProvider.Configure(config);

            //Initialize Json Configuration
            InitializeJsonConfig(config);

            //Initialize Unity Container
            InitializeUnityContainer(config);

            // Web API routes
            config.MapHttpAttributeRoutes();
        }

        private static void InitializeUnityContainer(HttpConfiguration configuration)
        {
            var container = new UnityContainer();
            container.RegisterType<IEmployeeBusinessLayer, EmployeeBusinessLayer>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmployeeRepository, EmployeeRepository>(new HierarchicalLifetimeManager());
            configuration.DependencyResolver = new UnityResolver(container);
        }

        private static void InitializeJsonConfig(HttpConfiguration configuration)
        {
            var json = configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        }

        private static void InitializeHandler(HttpConfiguration configuration)
        {
            // Web API configuration and services
            configuration.Filters.Add(new ValidateModelStateFilter());
            configuration.MessageHandlers.Add(new ResponseWrappingHandler());

            //GZip compression
            GlobalConfiguration.Configuration.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
        }

    }
}
