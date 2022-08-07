namespace Domain.Entities.GameEntities
{
    public class Game
    {
        public int Id { get; set; }
        public bool IsFinished { get; set; }
        public DateTime StartDate { get; set; }
        public int? NextPlayer { get; set; }

        #region Relations

        public ICollection<Player> Players { get; set; } = null!;

        #endregion
    }
}
