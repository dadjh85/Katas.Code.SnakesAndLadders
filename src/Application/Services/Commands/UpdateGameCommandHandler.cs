using Application.DtoModels.Models.UpdateGame;
using Domain.Entities.GameEntities;
using Domain.IRepositories;

namespace Application.Services.Commands
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameDto, Unit>
    {
        private readonly IGameRepository _gameRepository;

        #region constructor

        public UpdateGameCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }

        #endregion

        public async Task<Unit> Handle(UpdateGameDto request, CancellationToken cancellationToke)
        {
            Game? item = await _gameRepository.Find(request.Id);
            if (item != null)
            {
                item.IsFinished = request.IsFinished;
            }
            await _gameRepository.Update(item);
            return Unit.Value;
        }
    }
}
