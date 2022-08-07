using Application.DtoModels.Models.GetGame;
using SnakesAndLaddersGame.Configuration.Startup;
using SnakesAndLaddersGame.Services;

public static class Program
{
    public static async Task Main()
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        IConfiguration configurationApp = new ConfigurationBuilder().ConfigureAppsettings(environmentName);
        ServiceProvider serviceProvider = ServicesConfiguration.CreateNewServiceProvider(configurationApp);

        //start the game with the configuration selected
        GetGameDto game = await serviceProvider.GetService<IGamePipeLineService>().Game();

        //the players play
        await serviceProvider.GetService<IGamePipeLineService>().MovementPLayers(game);

    }
}
