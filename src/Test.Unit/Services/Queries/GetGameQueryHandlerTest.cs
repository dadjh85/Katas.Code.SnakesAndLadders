using Application.DtoModels.Models.GetGame;
using Application.Services.Queries;
using Domain.Entities.GameEntities;
using Domain.IRepositories;
using Infrastructure.Persistence.GameContextConfiguration;
using Infrastructure.Persistence.Repositories;
using Test.Unit.GlobalConfiguration;

namespace Test.Unit.Services.Queries
{
    [Collection("Sequential")]
    public class GetGameQueryHandlerTest
    {
        private readonly GameContext _context;
        private readonly GetGameQueryHandler _getGamehandler;
        private readonly IGameRepository _repository;
        private readonly IMapper _mapper;

        public GetGameQueryHandlerTest()
        {
            _context = ContextConfig.ContextConfigure<GameContext>(databaseName: "SnakeAndLadderGame");
            _repository = new GameRepository(_context);
            _mapper = GetIMapper();
            _getGamehandler = new GetGameQueryHandler(_repository, _mapper);
        }


        [Fact]
        public async Task GetGameQueryHandler_when_there_is_a_game()
        {
            var game = await AddGame();
            var item = new GetGameByIdDto
            {
                Id = game.Id
            };

            var result = await _getGamehandler.Handle(item, CancellationToken.None);

            result.Id.Should().Be(game.Id);
            result.Players.Count.Should().Be(2);
            result.Players.Any(sl => sl.Name == "David").Should().Be(true);
            result.Players.Any(sl => sl.Name == "Pepe").Should().Be(true);
        }

        [Fact]
        public void GetGameQueryHandler_When_repository_parameter_is_null()
        {
            GetGameQueryHandler handler;
            IGameRepository gameRepository = null;
            Assert.Throws<ArgumentNullException>(() => handler = new GetGameQueryHandler(gameRepository, _mapper));
        }

        [Fact]
        public void GetGameQueryHandler_When_automapper_parameter_is_null()
        {
            IMapper mapper = null;
            GetGameQueryHandler handler;
            Assert.Throws<ArgumentNullException>(() => handler = new GetGameQueryHandler(_repository, mapper));
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

        private IMapper GetIMapper()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddListProfiles(GetProfiles());
            });

            return new Mapper(configuration);
        }

        public static List<Profile> GetProfiles()
                  => new List<Profile>
                  {
                      new MapGetGameDto(),
                      new MapGetGamePlayerDto()
                  };
    }
}
