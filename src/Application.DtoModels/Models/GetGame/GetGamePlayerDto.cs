using Domain.Entities.GameEntities;

namespace Application.DtoModels.Models.GetGame
{
    public class GetGamePlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Position { get; set; }
        public int GameId { get; set; }
    }

    public class MapGetGamePlayerDto : Profile
    {
        public MapGetGamePlayerDto()
        {
            CreateMap<Player, GetGamePlayerDto>();
        }
    }
}
