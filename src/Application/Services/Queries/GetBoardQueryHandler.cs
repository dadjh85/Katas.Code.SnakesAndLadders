using Application.DtoModels.Models.GetBoard;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.IRepositories;

namespace Application.Services.Queries
{
    public class GetBoardQueryHandler : IRequestHandler<GetBoardByIdDto, GetBoardDto?>
    {
        private readonly IMapper _mapper;
        private readonly IBoardRepository _boardRepository;

        public GetBoardQueryHandler(IBoardRepository boardRepository, IMapper mapper)
        {
            _boardRepository = boardRepository ?? throw new ArgumentNullException(nameof(boardRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GetBoardDto?> Handle(GetBoardByIdDto request, CancellationToken cancellationToke)
            => await _boardRepository.Get(request.Id)
                                     .ProjectTo<GetBoardDto>(_mapper.ConfigurationProvider)
                                     .FirstOrDefaultAsync();
    }
}
