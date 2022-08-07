namespace Domain.Entities.BoardEntities
{
    public class Board
    {
        public int Id { get; set; }
        public int TotalBoxes { get; set; }

        #region Relations

        public ICollection<SnakeAndLader> SnakesAndLaders { get; set; } = null!;

        #endregion

    }
}
