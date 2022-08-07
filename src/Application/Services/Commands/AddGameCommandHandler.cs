using Application.DtoModels.Models.AddGame;
using Application.DtoModels.Models.GetGame;
using AutoMapper;
using Domain.Entities.GameEntities;
using Domain.IRepositories;

namespace Application.Services.Commands
{
    public class AddGameCommandHandler : IRequestHandler<AddGameDto, GetGameDto>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        #region constructor

        public AddGameCommandHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        public async Task<GetGameDto>  Handle(AddGameDto request, CancellationToken cancellationToke)
        {
            var item = _mapper.Map<Game>(request);
            return _mapper.Map<GetGameDto>(await _gameRepository.Add(item));
        }

    }
}
