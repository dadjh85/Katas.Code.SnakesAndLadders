using Application.DtoModels.Models.GetBoard;

namespace SnakesAndLaddersGame.Services
{
    public interface IBoardService
    {
        Task<GetBoardDto> GetBoardConfiguration(int id);
    }
}
