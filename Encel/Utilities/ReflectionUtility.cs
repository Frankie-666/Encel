using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Encel.Utilities
{
    public class ReflectionUtility
    {
        public static Lazy<List<Assembly>> LoadedAssemblies = new Lazy<List<Assembly>>(GetLoadedAssemblies);

        public List<Type> GetTypesImplementing(Type @interface, IEnumerable<Assembly> fromAssemblies = null)
        {
            if (fromAssemblies == null)
            {
                fromAssemblies = LoadedAssemblies.Value;
            }

            return fromAssemblies
                .SelectMany(s => s.GetTypes())
                .Where(type => @interface.IsAssignableFrom(type) && !type.IsInterface)
                .ToList();
        }

        private static List<Assembly> GetLoadedAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.GlobalAssemblyCache && // ignore assemblies in GAC
                    !assembly.FullName.StartsWith("System.")).ToList(); // ignore all System assemblies
        }

        public IEnumerable<TAttribute> GetAttributesForType<TAttribute>(Type type) where TAttribute : Attribute
        {
            return type.GetCustomAttributes<TAttribute>();
        }
    }
}
