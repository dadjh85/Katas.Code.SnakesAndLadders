namespace Application.DtoModels.Models.UpdateGame
{
    public class UpdateGameDto : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool IsFinished { get; set; }
    }
}
