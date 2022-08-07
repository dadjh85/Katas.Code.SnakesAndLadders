using Application.DtoModels.Models.AddGame;
using Application.DtoModels.Models.Enums;
using Application.DtoModels.Models.GetBoard;
using Application.DtoModels.Models.GetGame;
using Application.DtoModels.Models.UpdatePlayer;

namespace SnakesAndLaddersGame.Services
{
    public class GamePipeLineService : IGamePipeLineService
    {
        private readonly IGameService _gameService;
        private readonly IBoardService _boardService;
        private readonly IPlayerService _playerService;

        public GamePipeLineService(IGameService gameService, IBoardService boardService, IPlayerService playerService)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            _boardService = boardService ?? throw new ArgumentNullException(nameof(boardService));
            _playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
        }

        public async Task<GetGameDto> Game()
        {
            Console.WriteLine("**** WELCOME to SnackesAndLadders Game ****");
            Console.WriteLine("Select the number of Users: ");
            int totalUsers = 0;
            bool isNumeric = int.TryParse(Console.ReadLine(), out totalUsers);

            return await StartGame(totalUsers);
        }

        public async Task MovementPLayers(GetGameDto game)
        {
            GetBoardDto boardConfiguration = await _boardService.GetBoardConfiguration((int)EnumBoard.PrincipalBoard);
            while (!game.Players.Any(p => p.Position == boardConfiguration.TotalBoxes))
            {
                foreach (var itemPlayer in game.Players)
                {
                    Console.WriteLine($"Turn for player {itemPlayer.Name} press enter to roll the dice .....");
                    Console.ReadLine();
                    int numberDice = _gameService.RollDice();
                    Console.WriteLine($"the number of the dice is {numberDice} and your current position is: {itemPlayer.Position}");
                    await _playerService.UpdatePlayerPosition(boardConfiguration,
                                                              new UpdatePlayerDto
                                                              {
                                                                  Id = itemPlayer.Id,
                                                                  Position = itemPlayer.Position
                                                              },
                                                              numberDice);

                }

                game = await _gameService.GetGame(game.Id);
            }

            string nameWinnerPlayer = game.Players.Where(p => p.Position == boardConfiguration.TotalBoxes)
                                                  .FirstOrDefault().Name;


            
            await _gameService.FinishGame(game.Id);
            Console.WriteLine($" =========> Player {nameWinnerPlayer} is the winner.... <===========");
            Console.ReadLine();
        }

        #region Private Methods

        private async Task<GetGameDto> StartGame(int totalUsers)
        {
            var gameToStart = new AddGameDto();
            gameToStart.Players = new List<AddGamePlayerDto>();

            foreach (int user in new int[totalUsers])
            {
                Console.WriteLine($"Write the name of the User: ");
                string name = Console.ReadLine();

                if (!string.IsNullOrEmpty(name))
                    gameToStart.Players.Add(new AddGamePlayerDto { Name = name });
            }

            return await _gameService.StartGame(gameToStart);
        }

        #endregion


    }
}
