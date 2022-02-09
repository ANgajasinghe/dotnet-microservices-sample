using System.Reflection;
using AutoMapper;

namespace CommandsService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            
            ReflectMethods(assembly,typeof(IMapFrom<>),"IMapFrom`1");
            ReflectMethods(assembly,typeof(IMapTo<>),"IMapTo`1");
         
        }

        private void ReflectMethods(Assembly assembly,Type interfaceType, string interfaceName)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType))
                .ToList();
            
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                // check IMapTo or From First
                var clzMethod = type.GetMethod("Mapping");

                if (clzMethod is null)
                {

                    var interfaces = type.GetInterfaces()?
                        .Where(x=>x.Name.Equals(interfaceName));

                    if (interfaces is null) continue;
                    
                    foreach (var iT in interfaces)
                    {
                        var methodInfo = iT?.GetMethod("Mapping");
                        methodInfo?.Invoke(instance, new object[] { this });
                    }
                    
                }
                else
                {
                    clzMethod.Invoke(instance, new object[] { this });
                }

            }
        }
    }
}
