using Application.DtoModels.Models.AddGame;
using Application.DtoModels.Models.GetGame;
using Application.DtoModels.Models.UpdateGame;
using Kit.DotNet.Core.Utils.Extensions;
using Kit.DotNet.Core.Utils.Models;
using Microsoft.Extensions.Options;
using SnakesAndLaddersGame.Configuration.Settings;

namespace SnakesAndLaddersGame.Services
{
    public class GameService : IGameService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiGameSettings _apiGameSettings;

        private const string URL_API_GAME = "Game";

        public GameService(IHttpClientFactory httpClientFactory, IOptions<ApiGameSettings> apiGameSettings)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiGameSettings = apiGameSettings.Value ?? throw new ArgumentNullException(nameof(apiGameSettings)); 
        }

        public async Task<GetGameDto> StartGame(AddGameDto item)
        {
            var apiGameClient = _httpClientFactory.CreateClient(_apiGameSettings.Name);

            Response<GetGameDto> result = await apiGameClient.PostAsync<GetGameDto>(new RequestParameters
            {
                Url = URL_API_GAME,
                HttpContent = JsonExtension.SerializeObject(item).ToStringContent()
            });

            return result.Entity;
        }

        public async Task<GetGameDto> GetGame(int id)
        {
            var apiGameClient = _httpClientFactory.CreateClient(_apiGameSettings.Name);
            Response<GetGameDto> result = await apiGameClient.GetAsync<GetGameDto>(new RequestParameters
            {
                Url = $"{URL_API_GAME}/{id}"
            });
            return result.Entity;
        }

        public int RollDice()
        {
            Random r = new Random();
            return r.Next(1, 6);
        }

        public async Task FinishGame(int id)
        {
            var apiGameClient = _httpClientFactory.CreateClient(_apiGameSettings.Name);
            await apiGameClient.PutAsync<string>(new RequestParameters
            {
                Url = $"{URL_API_GAME}/{id}",
                HttpContent = JsonExtension.SerializeObject(new UpdateGameDto { IsFinished = true }).ToStringContent()
            });
        }
    }
}
