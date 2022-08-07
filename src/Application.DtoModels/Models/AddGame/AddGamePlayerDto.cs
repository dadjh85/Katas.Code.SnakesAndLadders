using Domain.Entities.GameEntities;

namespace Application.DtoModels.Models.AddGame
{
    public class AddGamePlayerDto
    {
        [Required]
        public string Name { get; set; } = null!;
    }

    public class MapAddPlayerDto : Profile
    {
        public MapAddPlayerDto()
        {
            CreateMap<AddGamePlayerDto, Player>();
        }
    }
}
