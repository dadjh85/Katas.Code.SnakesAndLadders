namespace API.Configuration.Startup
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddScrutor(this IServiceCollection services)
        {
            services.Scan(s => s.FromAssemblies(Assembly.Load(nameof(Infrastructure)), Assembly.Load(nameof(Domain)))
                    .AddClasses(c => c.Where(e => e.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }
    }
}
