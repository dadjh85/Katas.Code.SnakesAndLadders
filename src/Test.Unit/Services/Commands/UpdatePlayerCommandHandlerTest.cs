using Application.DtoModels.Models.UpdatePlayer;
using Application.Services.Commands;
using Domain.Entities.GameEntities;
using Domain.IRepositories;
using Infrastructure.Persistence.GameContextConfiguration;
using Infrastructure.Persistence.Repositories;
using Test.Unit.GlobalConfiguration;

namespace Test.Unit.Services.Commands
{
    [Collection("Sequential")]
    public class UpdatePlayerCommandHandlerTest
    {
        private readonly GameContext _context;
        private readonly UpdatePlayerCommandHandler _updatePlayerhandler;
        private readonly IPlayerRepository _repository;
        private readonly IGameRepository _repositoryGame;

        public UpdatePlayerCommandHandlerTest()
        {
            _context = ContextConfig.ContextConfigure<GameContext>(databaseName: "SnakeAndLadderGame");
            _repository = new PlayerRepository(_context);
            _repositoryGame = new GameRepository(_context);
            _updatePlayerhandler = new UpdatePlayerCommandHandler(_repository);
        }

        [Fact]
        public async Task UpdatePlayerCommandHandler_when_player_propertie_Position_50()
        {
            Game game = await AddGameWithPlayer();
            var playerLucia = game.Players.FirstOrDefault(p => p.Name == "Lucia");
            var item = new UpdatePlayerDto()
            {
                Id = playerLucia.Id,
                Position = 50
            };

            await _updatePlayerhandler.Handle(item, CancellationToken.None);

            playerLucia.Position.Should().Be(50);
        }

        [Fact]
        public void UpdatePlayerCommandHandler_When_repository_parameter_is_null()
        {
            UpdatePlayerCommandHandler handler;
            IPlayerRepository payerRepository = null;
            Assert.Throws<ArgumentNullException>(() => handler = new UpdatePlayerCommandHandler(payerRepository));
        }

        private async Task<Game> AddGameWithPlayer()
        {
            var item = new Game()
            {
                Players = new List<Player>
                {
                   new Player { Name = "Paco" },
                   new Player { Name = "Lucia" }
                }
            };

            await _repositoryGame.Add(item);

            return item;
        }
    }
}
