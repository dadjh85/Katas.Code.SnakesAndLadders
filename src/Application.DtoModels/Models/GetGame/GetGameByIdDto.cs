using Application.DtoModels.Models.Common;

namespace Application.DtoModels.Models.GetGame
{
    public class GetGameByIdDto : GenericIdDto, IRequest<GetGameDto>
    {
    }
}
