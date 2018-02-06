using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using System.Text;
using System.Threading.Tasks;

namespace Task.Common
{
    public static class AutoMapperFactory
    {
        public static IMapper Create(Assembly entryAssembly, IKernel kernel = null, 
            Action<IMapperConfigurationExpression> additionalActions = null)
        {
            var config = new MapperConfiguration(configuration => 
            LoadProfiles(configuration, entryAssembly, kernel, additionalActions));
            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();
            if (kernel != null)
            {
                kernel.Bind<IMapper>().ToConstant(mapper);
            }

            Mapper.Initialize(configuration => LoadProfiles(configuration, entryAssembly, kernel, additionalActions));

            return mapper;
        }

        private static void LoadProfiles(
            IMapperConfigurationExpression configuration,
            Assembly entryAssembly,
            IKernel kernel,
            Action<IMapperConfigurationExpression> additionalActions
        )
        {
            var profiles = new List<Profile>();
            var profileType = typeof(Profile);

            var assemblies = AssemblyLoader.GetAssemblies(entryAssembly);
            foreach (var assembly in assemblies)
            {
                profiles.AddRange(
                    AssemblyLoader.GetLoadableTypes(assembly)
                        .Where(profileType.IsAssignableFrom)
                        .Where(t => t.GetConstructor(Type.EmptyTypes) != null)
                        .Select(Activator.CreateInstance)
                        .Cast<Profile>()
                );
            }

            if (kernel != null)
            {
                configuration.ConstructServicesUsing(type => kernel.Get(type));
            }
            foreach (var profile in profiles)
            {
                configuration.AddProfile(profile);
            }

            if (additionalActions != null)
            {
                additionalActions(configuration);
            }
        }
    }
}
