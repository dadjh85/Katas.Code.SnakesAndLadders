using Application.DtoModels.Models.GetBoard;
using Application.DtoModels.Models.UpdatePlayer;
using Kit.DotNet.Core.Utils.Extensions;
using Kit.DotNet.Core.Utils.Models;
using Microsoft.Extensions.Options;
using SnakesAndLaddersGame.Configuration.Settings;

namespace SnakesAndLaddersGame.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiGameSettings _apiGameSettings;

        private const string URL_API_PLAYER = "Player";

        public PlayerService(IHttpClientFactory httpClientFactory, IOptions<ApiGameSettings> apiGameSettings)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _apiGameSettings = apiGameSettings.Value ?? throw new ArgumentNullException(nameof(apiGameSettings));
        }

        public async Task UpdatePlayerPosition(GetBoardDto board, UpdatePlayerDto item, int numberDice)
        {
            var apiGameClient = _httpClientFactory.CreateClient(_apiGameSettings.Name);

            item.Position = CalculatePosition(board, numberDice, item.Position);

            await apiGameClient.PutAsync<string>(new RequestParameters
            {
                Url = $"{URL_API_PLAYER}/{item.Id}",
                HttpContent = JsonExtension.SerializeObject(item).ToStringContent()
            });
        }

        private int CalculatePosition(GetBoardDto board, int numberDice, int currentPosition)
        {
            int boxSelected = currentPosition + numberDice;
            if (board.SnakesAndLaders.Any(sl => sl.StartBox == boxSelected))
            {

                var item = board.SnakesAndLaders.Where(sl => sl.StartBox == boxSelected)
                                                .FirstOrDefault();

                string snakeAndLeaderName = item.IsLadder ? "Leader" : "Snake";
                Console.WriteLine($"you have landed in the {boxSelected} box and there is an {snakeAndLeaderName} your new position is: {item.EndBox}");
                return item.EndBox;
            }
            else if (boxSelected > board.TotalBoxes)
            {
                int numberToWin = board.TotalBoxes - currentPosition;
                Console.WriteLine($"you must get a {numberToWin} to win the game");
                return currentPosition;
            }
            else
            {
                return currentPosition + numberDice;
            }
        }
    }
}
