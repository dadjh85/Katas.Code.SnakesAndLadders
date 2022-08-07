using Domain.Entities.BoardEntities;

namespace Application.DtoModels.Models.GetBoard
{
    public class GetBoardDto
    {
        public int Id { get; set; }
        public int TotalBoxes { get; set; }
        public ICollection<GetBoardSnakeAndLaderDto> SnakesAndLaders { get; set; } = null!;
    }

    public class MapGetBoardDto : Profile
    {
        public MapGetBoardDto()
        {
            CreateMap<Board, GetBoardDto>();
        }
    }
}
