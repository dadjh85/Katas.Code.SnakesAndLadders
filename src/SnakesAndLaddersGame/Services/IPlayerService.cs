using Application.DtoModels.Models.GetBoard;
using Application.DtoModels.Models.UpdatePlayer;

namespace SnakesAndLaddersGame.Services
{
    public interface IPlayerService
    {
        Task UpdatePlayerPosition(GetBoardDto board, UpdatePlayerDto item, int numberDice);
    }
}
