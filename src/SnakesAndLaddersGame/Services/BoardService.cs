using Application.DtoModels.Models.GetBoard;
using Kit.DotNet.Core.Utils.Models;
using Microsoft.Extensions.Options;
using SnakesAndLaddersGame.Configuration.Settings;
using Kit.DotNet.Core.Utils.Extensions;

namespace SnakesAndLaddersGame.Services
{
    public class BoardService : IBoardService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiGameSettings _apiGameSettings;

        private const string URL_API_GAME = "Board";

        public BoardService(IHttpClientFactory httpClientFactory, IOptions<ApiGameSettings> apiGameSettings)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiGameSettings = apiGameSettings.Value ?? throw new ArgumentNullException(nameof(apiGameSettings));
        }

        public async Task<GetBoardDto> GetBoardConfiguration(int id)
        {
            var apiGameClient = _httpClientFactory.CreateClient(_apiGameSettings.Name);
            Response<GetBoardDto> result = await apiGameClient.GetAsync<GetBoardDto>(new RequestParameters
            {
                Url = $"{URL_API_GAME}/{id}"
            });
            return result.Entity;
        }
    }
}
