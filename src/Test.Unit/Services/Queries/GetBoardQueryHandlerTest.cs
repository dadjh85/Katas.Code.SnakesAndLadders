using Application.DtoModels.Models.GetBoard;
using Application.Services.Queries;
using Domain.Entities.BoardEntities;
using Domain.IRepositories;
using Infrastructure.Persistence.BoardContextConfiguration;
using Infrastructure.Persistence.Repositories;
using Test.Unit.GlobalConfiguration;

namespace Test.Unit.Services.Queries
{
    [Collection("Sequential")]
    public class GetBoardQueryHandlerTest
    {
        private readonly BoardContext _context;
        private readonly IBoardRepository _repository;
        private readonly IMapper _mapper;
        private readonly GetBoardQueryHandler _getBoardhandler;

        public GetBoardQueryHandlerTest()
        {
            _context = ContextConfig.ContextConfigure<BoardContext>(databaseName: "SnakeAndLadderGame");
            _repository = new BoardRepository(_context);
            _mapper = GetIMapper();
            _getBoardhandler = new GetBoardQueryHandler(_repository, _mapper);
        }


        [Fact]
        public async Task GetBoardQueryHandler_when_there_is_a_board()
        {
            var board = await AddBoard();
            var item = new GetBoardByIdDto
            {
                Id = board.Id
            };
            
            var result = await _getBoardhandler.Handle(item, CancellationToken.None);

            result.Id.Should().Be(board.Id);
            result.SnakesAndLaders.Count.Should().Be(2);
            result.SnakesAndLaders.Any(sl => sl.Id == 1).Should().Be(true);
            result.SnakesAndLaders.Any(sl => sl.Id == 2).Should().Be(true);
        }

        [Fact]
        public void GetBoardQueryHandler_When_repository_parameter_is_null()
        {
            GetBoardQueryHandler handler;
            IBoardRepository boardRepository = null;
            Assert.Throws<ArgumentNullException>(() => handler = new GetBoardQueryHandler(boardRepository, _mapper));
        }

        [Fact]
        public void GetBoardQueryHandler_When_automapper_parameter_is_null()
        {
            IMapper mapper = null;
            GetBoardQueryHandler handler;
            Assert.Throws<ArgumentNullException>(() => handler = new GetBoardQueryHandler(_repository, mapper));
        }

        [Fact]
        public async Task GetBoardQueryHandler_when_there_is_no_data()
        {
            var item = new GetBoardByIdDto
            {
                Id = 1
            };

            var result = await _getBoardhandler.Handle(item, CancellationToken.None);

            result.Should().BeNull();
        }

        private async Task<Board> AddBoard()
        {
            var board = new Board()
            {
                Id = 1,
                TotalBoxes = 100,
                SnakesAndLaders = new List<SnakeAndLader>
                {
                    new SnakeAndLader {  Id = 1, StartBox = 5, EndBox = 10, IsLadder = false },
                    new SnakeAndLader {  Id = 2, StartBox = 8, EndBox = 4, IsLadder = true }
                }
            };

            var item = _context.Board.Add(board);
            await _context.SaveChangesAsync();

            return item.Entity;
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
                      new MapGetBoardDto(),
                      new MapGetBoardSnakeAndLaderDto()
                  };
    }
}
