using Application.DtoModels.Models.GetGame;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.IRepositories;

namespace Application.Services.Queries
{
    public class GetGameQueryHandler : IRequestHandler<GetGameByIdDto, GetGameDto?>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        #region constructor

        public GetGameQueryHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        public async Task<GetGameDto?> Handle(GetGameByIdDto request, CancellationToken cancellationToke)
            => await _gameRepository.Get(request.Id)
                                    .ProjectTo<GetGameDto>(_mapper.ConfigurationProvider)
                                    .FirstOrDefaultAsync();
    }
}
