using Domain.Entities.BoardEntities;

namespace Domain.IContexts
{
    public interface IBoardContext
    {
        public DbSet<Board> Board { get; set; }
        public DbSet<SnakeAndLader> SnakeAndLader { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
