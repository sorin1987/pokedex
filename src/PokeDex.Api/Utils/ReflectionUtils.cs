using System.Linq;
using System.Reflection;

namespace PokeDex.Api.Utils
{
    public static class ReflectionUtils
    {
        public static string GetAssemblyVersion<T>()
        {
            var containingAssembly = typeof(T).GetTypeInfo().Assembly;
            
            return containingAssembly
                .GetCustomAttributes<AssemblyInformationalVersionAttribute>()
                .FirstOrDefault()?
                .InformationalVersion;
        }
    }
}