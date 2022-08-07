using Domain.Entities.BoardEntities;

namespace Application.DtoModels.Models.GetBoard
{
    public class GetBoardSnakeAndLaderDto
    {
        public int Id { get; set; }
        public int StartBox { get; set; }
        public int EndBox { get; set; }
        public bool IsLadder { get; set; }
    }

    public class MapGetBoardSnakeAndLaderDto : Profile
    {
        public MapGetBoardSnakeAndLaderDto()
        {
            CreateMap<SnakeAndLader, GetBoardSnakeAndLaderDto>();
        }
    }
}
