using Application.DtoModels.Models.UpdateGame;
using Application.Services.Commands;
using Domain.Entities.GameEntities;
using Domain.IRepositories;
using Infrastructure.Persistence.GameContextConfiguration;
using Infrastructure.Persistence.Repositories;
using Test.Unit.GlobalConfiguration;

namespace Test.Unit.Services.Commands
{
    [Collection("Sequential")]
    public class UpdateGameCommandHandlerTest
    {
        private readonly GameContext _context;
        private readonly UpdateGameCommandHandler _updateGamehandler;
        private readonly IGameRepository _repository;

        public UpdateGameCommandHandlerTest()
        {
            _context = ContextConfig.ContextConfigure<GameContext>(databaseName: "SnakeAndLadderGame");
            _repository = new GameRepository(_context);
            _updateGamehandler = new UpdateGameCommandHandler(_repository);
        }

        [Fact]
        public async Task UpdateGameCommandHandler_when_game_propertie_IsFinised_true()
        {
            Game game = await AddGame();
            var item = new UpdateGameDto
            {
                Id = game.Id,
                IsFinished = true
            };

            await _updateGamehandler.Handle(item, CancellationToken.None);

            game.IsFinished.Should().Be(true);
        }

        [Fact]
        public async Task UpdateGameCommandHandler_when_game_propertie_IsFinised_false()
        {
            Game game = await AddGame();
            var item = new UpdateGameDto
            {
                Id = game.Id,
                IsFinished = false
            };

            await _updateGamehandler.Handle(item, CancellationToken.None);

            game.IsFinished.Should().Be(false);
        }

        [Fact]
        public void UpdateGameCommandHandler_When_repository_parameter_is_null()
        {
            UpdateGameCommandHandler handler;
            IGameRepository gamerepository = null;
            Assert.Throws<ArgumentNullException>(() => handler = new UpdateGameCommandHandler(gamerepository));
        }

        private async Task<Game> AddGame()
        {
            var item = new Game()
            {
                Players = new List<Player>
                {
                   new Player { Name = "David" },
                   new Player { Name = "Pepe" }
                }
            };

            await _repository.Add(item);

            return item;
        }
    }
}
