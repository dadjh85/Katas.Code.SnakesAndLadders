using Domain.Entities.GameEntities;

namespace Domain.IContexts
{
    public interface IGameContext
    {
        DbSet<Game> Game { get; set; } 
        DbSet<Player> Player { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
