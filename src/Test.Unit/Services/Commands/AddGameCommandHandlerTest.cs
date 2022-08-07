using Application.DtoModels.Models.AddGame;
using Application.DtoModels.Models.GetGame;
using Application.Services.Commands;
using Domain.IRepositories;
using Infrastructure.Persistence.GameContextConfiguration;
using Infrastructure.Persistence.Repositories;
using Test.Unit.GlobalConfiguration;

namespace Test.Unit.Services.Commands
{
    [Collection("Sequential")]
    public class AddGameCommandHandlerTest
    {
        private readonly GameContext _context;
        private readonly AddGameCommandHandler _addGamehandler;
        private readonly IMapper _mapper;
        private readonly IGameRepository _repository;

        public AddGameCommandHandlerTest()
        {
            _context = ContextConfig.ContextConfigure<GameContext>(databaseName: "SnakeAndLadderGame");
            _repository = new GameRepository(_context);
            _mapper = GetIMapper();
            _addGamehandler = new AddGameCommandHandler(_repository, _mapper);
        }

        [Fact]
        public async Task AddGameCommandHandler_when_the_game_have_two_players()
        {
            var item = new AddGameDto 
            {
                Players = new List<AddGamePlayerDto> 
                {
                    new AddGamePlayerDto { Name = "David" },
                    new AddGamePlayerDto { Name = "Pepe" }
                }
            };
            await _addGamehandler.Handle(item, CancellationToken.None);

            var games = await _context.Game.ToListAsync();
            games.Count.Should().Be(1);
            var players = games.FirstOrDefault().Players;
            players.Count.Should().Be(2);
            players.Any(p => p.Name == "David").Should().Be(true);
            players.Any(p => p.Name == "Pepe").Should().Be(true);
        }

        [Fact]
        public void AddGameCommandHandler_When_repository_parameter_is_null()
        {
            AddGameCommandHandler handler;
            IGameRepository gamerepository = null;
            Assert.Throws<ArgumentNullException>(() => handler = new AddGameCommandHandler(gamerepository, _mapper));
        }

        [Fact]
        public void AddGameCommandHandler_When_automapper_parameter_is_null()
        {
            IMapper mapper = null;
            AddGameCommandHandler handler;
            Assert.Throws<ArgumentNullException>(() => handler = new AddGameCommandHandler(_repository, mapper));
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
                      new MapAddGameDto(),
                      new MapGetGameDto(),
                      new MapAddPlayerDto(),
                      new MapGetGamePlayerDto()
                  };
    }
}
