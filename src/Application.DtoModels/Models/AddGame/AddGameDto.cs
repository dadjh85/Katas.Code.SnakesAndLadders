using Application.DtoModels.Models.GetGame;
using Domain.Entities.GameEntities;

namespace Application.DtoModels.Models.AddGame
{
    public class AddGameDto : IRequest<GetGameDto>
    {
        public ICollection<AddGamePlayerDto> Players { get; set; } = null!;
    }

    public class MapAddGameDto : Profile
    {
        public MapAddGameDto()
        {
            CreateMap<AddGameDto, Game>();
        }
    }
}
