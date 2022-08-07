using Microsoft.Extensions.DependencyInjection;

namespace SnakesAndLaddersGame.Configuration.Startup
{
    public static class ServicesConfiguration
    {
        public static IConfiguration ConfigureAppsettings(this ConfigurationBuilder configurationBuilder, string environmentName)
        {
            const string CONFIGURATION_FILE_NAME = "appsettings";

            return configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                                       .AddJsonFile($"{CONFIGURATION_FILE_NAME}.json", optional: false, reloadOnChange: true)
                                       .AddJsonFile($"{CONFIGURATION_FILE_NAME}.{environmentName}.json", optional: false, reloadOnChange: true).Build();
      
        }

        #region Dependency Injection

        public static ServiceProvider CreateNewServiceProvider(IConfiguration configuration)
            => new ServiceCollection().ConfigureService(configuration)
                                      .BuildServiceProvider();

        #endregion
    }
}
