using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task.Common
{
    public static class NinjectFactory
    {
        public static StandardKernel Create(Assembly entryAssembly)
        {
            var assemblies = AssemblyLoader.GetAssemblies(entryAssembly);
            var moduleType = typeof(INinjectModule);
            var markerType = typeof(ITaskNinjectModule);
            var modules = new List<INinjectModule>();

            foreach (var assembly in assemblies)
            {
                modules.AddRange(
                    AssemblyLoader.GetLoadableTypes(assembly)
                        .Where(moduleType.IsAssignableFrom)
                        .Where(markerType.IsAssignableFrom)
                        .Where(t => t.GetConstructor(Type.EmptyTypes) != null)
                        .Select(Activator.CreateInstance)
                        .Cast<INinjectModule>()
                );
            }

            return new StandardKernel(modules.ToArray());
        }
    }
}
