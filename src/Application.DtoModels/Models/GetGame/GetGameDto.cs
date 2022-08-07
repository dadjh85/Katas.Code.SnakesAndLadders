using Domain.Entities.GameEntities;

namespace Application.DtoModels.Models.GetGame
{
    public class GetGameDto
    {
        public int Id { get; set; }
        public bool IsFinished { get; set; }
        public int? NextPlayer { get; set; }
        public List<GetGamePlayerDto>? Players { get; set; }

    }

    public class MapGetGameDto : Profile
    {
        public MapGetGameDto()
        {
            CreateMap<Game, GetGameDto>();
        }
    }
}
