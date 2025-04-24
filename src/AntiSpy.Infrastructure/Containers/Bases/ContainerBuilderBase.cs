using Microsoft.Extensions.DependencyInjection;
using AntiSpy.Infrastructure.Containers.LifeScoped;
using System.Reflection;

namespace AntiSpy.Infrastructure.Bases
{
    public static class ContainerBuilderBase
    {
        private static readonly Type _dependency = typeof(IDependency);
        private static readonly Type _singletonDependency = typeof(ISingletonDependency);
        private static readonly Type _scopedDependency = typeof(IScopedDependency);
        private static readonly Type _transientDependency = typeof(ITransientDependency);
        public static void InitCoreComponents(this IServiceCollection service)
        {
            var exportTypes = GetAllTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(IDependency).IsAssignableFrom(t)).ToList();
            foreach (var type in exportTypes)
            {
                RegisterTypeWithAutoResolve(ref service, type);
            }
        }
        static void RegisterTypeWithAutoResolve(ref IServiceCollection services, Type implementer)
        {
            if (!implementer.IsClass
                || implementer.IsAbstract
                || !_dependency.IsAssignableFrom(implementer))
                return;

            if (_dependency.IsAssignableFrom(implementer)
                || _singletonDependency.IsAssignableFrom(implementer)
                || _scopedDependency.IsAssignableFrom(implementer)
                || _transientDependency.IsAssignableFrom(implementer))
            {
                if (TryGetServiceLifetimeFromClassHierarcy(implementer, out var lifeTime))
                {
                    Type genericTypeIntf = null;
                    if (implementer.IsGenericType && implementer.IsGenericTypeDefinition)
                    {
                        genericTypeIntf = implementer.GetGenericTypeDefinition();
                    }

                    if (genericTypeIntf != null && genericTypeIntf.IsGenericTypeDefinition)
                    {
                        return;
                    }
                    else
                    {
                        var injects = ServiceDescriptor.Describe(implementer, implementer, lifeTime);
                        services.Add(injects);
                    }
                }
            }

            foreach (var interf in implementer.GetInterfaces())
            {
                //Do not process if type does not implement one of Depency types
                if (!_dependency.IsAssignableFrom(interf))
                    continue;

                //Do not process if type IS exactly Depency types
                if (interf.IsAssignableFrom(_dependency)
                || interf.IsAssignableFrom(_singletonDependency)
                || interf.IsAssignableFrom(_scopedDependency)
                || interf.IsAssignableFrom(_transientDependency))
                    continue;

                //Do not process if type does not implement a life time scoped dependency
                if (!TryGetServiceLifetimeFromClassHierarcy(implementer, out var lifeTime))
                    continue;

                Type genericTypeIntf = null;
                if (interf.IsGenericType && implementer.IsGenericTypeDefinition)
                {
                    genericTypeIntf = interf.GetGenericTypeDefinition();
                }

                if (genericTypeIntf != null && genericTypeIntf.IsGenericTypeDefinition)
                {
                    continue;
                }
                else
                {
                    var injects = ServiceDescriptor.Describe(interf, implementer, lifeTime);
                    services.Add(injects);
                }
            }
        }

        static bool TryGetServiceLifetimeFromClassHierarcy(Type type, out ServiceLifetime serviceLifetime)
        {
            if (_singletonDependency.GetTypeInfo().IsAssignableFrom(type))
            {
                serviceLifetime = ServiceLifetime.Singleton;
                return true;
            }
            if (_scopedDependency.GetTypeInfo().IsAssignableFrom(type))
            {
                serviceLifetime = ServiceLifetime.Scoped;
                return true;
            }
            if (_transientDependency.GetTypeInfo().IsAssignableFrom(type))
            {
                serviceLifetime = ServiceLifetime.Transient;
                return true;
            }
            serviceLifetime = ServiceLifetime.Transient;
            return false;
        }

        public static List<TypeInfo> GetAllTypes()
        {
            var assembliesToScan = AppDomain.CurrentDomain.GetAssemblies();
            var assembliesToScanArray = assembliesToScan as Assembly[] ?? assembliesToScan?.ToArray();

            if (assembliesToScanArray != null && assembliesToScanArray.Length > 0)
            {
                return assembliesToScanArray
                    .Where(a => !a.IsDynamic)
                    .Distinct()
                    .SelectMany(a => a.DefinedTypes)
                    .ToList();
            }
            return new List<TypeInfo>();
        }
    }
}
