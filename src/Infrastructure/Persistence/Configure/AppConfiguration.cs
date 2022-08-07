using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Persistence.Configure
{
    public static class AppConfiguration
    {
        /// <summary>
        /// Register all context of a assembly where a services of app.
        /// </summary>
        /// <typeparam name="TContext">Type of context to find in the assembly.</typeparam>
        /// <param name="services">Collection of services of the app.</param>
        /// <param name="assembly">Assembly to find the contexts.</param>
        /// <param name="optionsAction">Actions associed to registraton the context.</param>
        /// <param name="contextLifetime">Life time of context in the IoC.</param>
        /// <param name="optionsLifetime">Life time of the opctions the configuration the context in the IoC.</param>
        /// <returns><see cref="IServiceCollection" />to make chained calls.</returns>
        public static IServiceCollection AddAllDbContexts<TContext>(this IServiceCollection services,
                                                                    Assembly assembly,
                                                                    Action<DbContextOptionsBuilder> optionsAction,
                                                                    ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
                                                                    ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContext : class
        {

            InvokeAllAddDbContexts<TContext>(services, assembly, optionsAction, contextLifetime, optionsLifetime);

            return services;
        }

        /// <summary>
        /// Register all context of a assembly where a services of app with a list of context options
        /// </summary>
        /// <typeparam name="TContext">Type of context to find in the assembly.</typeparam>
        /// <param name="services">Collection of services of the app.</param>
        /// <param name="assembly">Assembly to find the contexts.</param>
        /// <param name="optionsContexts">A list of actions associed to registraton the context.</param>
        /// <param name="contextLifetime">Life time of context in the IoC.</param>
        /// <param name="optionsLifetime">Life time of the opctions the configuration the context in the IoC.</param>
        /// <returns><see cref="IServiceCollection" />to make chained calls.</returns>
        public static IServiceCollection AddAllDbContexts<TContext>(this IServiceCollection services,
                                                                    Assembly assembly,
                                                                    List<Action<DbContextOptionsBuilder>> optionsContexts,
                                                                    ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
                                                                    ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContext : class
        {


            foreach (var option in optionsContexts)
            {
                InvokeAllAddDbContexts<TContext>(services, assembly, option, contextLifetime, optionsLifetime);
            }

            return services;
        }

        private static void InvokeAllAddDbContexts<TContext>(IServiceCollection services,
                                                             Assembly assembly,
                                                             Action<DbContextOptionsBuilder> optionsAction,
                                                             ServiceLifetime contextLifetime,
                                                             ServiceLifetime optionsLifetime) where TContext : class
        {
            assembly.GetDerivedTypes<TContext>()
                   .ToList()
                   .ForEach(contextType =>
                   {
                       MethodInfo addDbContextMethod = typeof(EntityFrameworkServiceCollectionExtensions).GetMethods(BindingFlags.Public | BindingFlags.Static)
                                                                                                         .First(m => m.Name == "AddDbContext" &&
                                                                                                                     m.IsGenericMethod &&
                                                                                                                     m.GetParameters().Any(p => p.ParameterType == typeof(Action<DbContextOptionsBuilder>)) &&
                                                                                                                     m.GetGenericArguments().FirstOrDefault()?.Name == "TContext");

                       addDbContextMethod.MakeGenericMethod(contextType)
                                         .Invoke(obj: services, parameters: new object[] { services, optionsAction, contextLifetime, optionsLifetime });
                   });
        }

        /// <summary>
        /// Register all the context of a assembly with a services of the app and enabled the pool the contections for the same.    
        /// </summary>
        /// <typeparam name="TContext">Type of Context to find in the assembly.</typeparam>
        /// <param name="services">Collection the services of the app.</param>
        /// <param name="assembly">assembly to find the contexts.</param>
        /// <param name="optionsAction">Actions associed to registraton the context.</param>
        /// <returns><see cref="IServiceCollection" />to make chained calls.</returns>
        public static IServiceCollection AddAllDbContextsPool<TContext>(this IServiceCollection services,
                                                                        Assembly assembly,
                                                                        Action<DbContextOptionsBuilder> optionsAction) where TContext : class
        {
            assembly.GetDerivedTypes<TContext>()
                    .ToList()
                    .ForEach(contextType =>
                    {
                        MethodInfo addDbContextPoolMethod = typeof(EntityFrameworkServiceCollectionExtensions).GetMethods(BindingFlags.Public | BindingFlags.Static)
                                                                                                              .First(m => m.Name == "AddDbContextPool" &&
                                                                                                                          m.IsGenericMethod &&
                                                                                                                          m.GetParameters().Any(p => p.ParameterType == typeof(Action<DbContextOptionsBuilder>)) &&
                                                                                                                          m.GetGenericArguments().FirstOrDefault()?.Name == "TContext");

                        object? poolSize = addDbContextPoolMethod.GetParameters().First(p => p.Name == "poolSize").DefaultValue;
                        addDbContextPoolMethod?.MakeGenericMethod(contextType)
                                              .Invoke(obj: services, parameters: new[] { services, optionsAction, poolSize });
                    });

            return services;
        }        

        /// <summary>
        ///     Obtains the assembly type list which inherit form specified type.
        /// </summary>
        /// <typeparam name="TBase">Class type from which to check inheritance of objec</typeparam>
        /// <param name="assembly">Assembly type object</param>
        /// <returns>Type type <see cref="IEnumerable{T}" /></returns>
        private static IEnumerable<Type> GetDerivedTypes<TBase>(this Assembly assembly)
            => assembly.GetDerivedTypes(typeof(TBase));

        /// <summary>
        ///     Obtains the assembly type list which inherit form specified type.
        /// </summary>
        /// <param name="assembly">Assembly type object</param>
        /// <param name="baseType"><see cref="Type" /> type object</param>
        /// <returns>Type type <see cref="IEnumerable{T}" /></returns>
        private static IEnumerable<Type> GetDerivedTypes(this Assembly assembly, Type baseType)
            => assembly.GetTypes()
                       .Where(item => baseType.IsGenericType && item.BaseType != null && item.BaseType.IsGenericType
                                          ? item.BaseType.GetGenericTypeDefinition() == baseType
                                          : item.IsSubclassOf(baseType));
    }
}
