using Application.DtoModels.Models.GetGame;

namespace SnakesAndLaddersGame.Services
{
    public interface IGamePipeLineService
    {
        Task<GetGameDto> Game();

        Task MovementPLayers(GetGameDto game);
    }
}
