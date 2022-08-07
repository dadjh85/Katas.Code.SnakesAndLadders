namespace Domain.Entities.BoardEntities
{
    public class SnakeAndLader
    {
        public int Id { get; set; }
        public int StartBox { get; set; }
        public int EndBox { get; set; }
        public bool IsLadder { get; set; }

        #region Relations 

        public ICollection<Board> Boards { get; set; } = null!;

        #endregion
    }
}
