using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task.Common
{
    public static class AssemblyLoader
    {
        public static IEnumerable<Assembly> GetAssemblies(Assembly entryAssembly)
        {
            var list = new List<string>();
            var stack = new Stack<Assembly>();

            stack.Push(entryAssembly);

            do
            {
                var assembly = stack.Pop();
                yield return assembly;

                foreach (var reference in assembly.GetReferencedAssemblies())
                {
                    if (!list.Contains(reference.FullName))
                    {
                        Assembly refAssembly = null;

                        try
                        {
                            refAssembly = Assembly.Load(reference);
                        }
                        catch
                        {
                        }

                        if (refAssembly != null)
                        {
                            stack.Push(refAssembly);
                            list.Add(reference.FullName);
                        }
                    }
                }
            } while (stack.Count > 0);
        }


        public static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}
