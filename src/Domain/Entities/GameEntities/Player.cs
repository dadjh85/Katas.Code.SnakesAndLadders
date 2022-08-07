namespace Domain.Entities.GameEntities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Position { get; set; } = 1;

        #region Relation Properties

        public int GameId { get; set; }

        #endregion

        #region Relations

        public Game GameNavigation { get; set; } = null!;

        #endregion
    }
}
