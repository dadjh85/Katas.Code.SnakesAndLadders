using Application.DtoModels.Models.Common;

namespace Application.DtoModels.Models.GetBoard
{
    public class GetBoardByIdDto : GenericIdDto, IRequest<GetBoardDto>
    {
    }
}
