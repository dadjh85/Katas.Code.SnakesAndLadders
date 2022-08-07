using System.Text.Json.Serialization;

namespace Application.DtoModels.Models.UpdatePlayer
{
    public class UpdatePlayerDto : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int Position { get; set; }
    }
}
