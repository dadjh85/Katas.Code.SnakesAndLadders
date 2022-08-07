using SnakesAndLaddersGame.Configuration.Constants;
using SnakesAndLaddersGame.Configuration.Settings;
using System.Net.Http.Headers;

namespace SnakesAndLaddersGame.Configuration.Startup
{
    public static class IServiceCollectionExtension
    {
        #region Configure of services

        /// <summary>
        /// IoC of dependency injection of console aplication
        /// </summary>
        /// <param name="services"></param>
        /// <returns>a object IServiceCollection</returns>
        public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScrutor();
            services.AddApiSnakeAndLadder(configuration);
            services.AddSingleton(configuration);

            return services;
        }

        #endregion

        #region External Services

        public static IServiceCollection AddScrutor(this IServiceCollection services)
        {
            services.Scan(s => s.FromAssemblies(Assembly.Load(nameof(SnakesAndLaddersGame)))
                    .AddClasses(c => c.Where(e => e.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }


        public static IServiceCollection AddApiSnakeAndLadder(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiGameSettings>(configuration.GetSection(ConstanstsStartup.API_CONFIGURATION_SECTION_NAME));
            ApiGameSettings apiGameSettings = new ApiGameSettings();
            configuration.GetSection(ConstanstsStartup.API_CONFIGURATION_SECTION_NAME).Bind(apiGameSettings);

            services.AddHttpClient(apiGameSettings.Name, client =>
            {
                client.BaseAddress = new Uri(apiGameSettings.BaseUrl);

                if (!string.IsNullOrEmpty(apiGameSettings.MimeType))
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(apiGameSettings.MimeType));
            });

            return services;
        }

        #endregion
    }
}
