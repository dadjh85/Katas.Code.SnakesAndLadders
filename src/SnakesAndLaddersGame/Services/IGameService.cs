using Application.DtoModels.Models.AddGame;
using Application.DtoModels.Models.GetGame;

namespace SnakesAndLaddersGame.Services
{
    public interface IGameService
    {
        Task<GetGameDto> StartGame(AddGameDto item);
        int RollDice();

        Task<GetGameDto> GetGame(int id);

        Task FinishGame(int id);
    }
}
