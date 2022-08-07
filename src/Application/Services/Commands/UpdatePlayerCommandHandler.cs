using Application.DtoModels.Models.UpdatePlayer;
using AutoMapper;
using Domain.Entities.GameEntities;
using Domain.IRepositories;

namespace Application.Services.Commands
{
    public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerDto, Unit>
    {
        private readonly IPlayerRepository _playerRepository;

        #region constructor

        public UpdatePlayerCommandHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        #endregion

        public async Task<Unit> Handle(UpdatePlayerDto request, CancellationToken cancellationToke)
        {
            Player? item = await _playerRepository.Find(request.Id);
            if(item != null)
            {
                item.Position = request.Position;
            }
            await _playerRepository.Update(item);
            return Unit.Value;
        }
    }
}
