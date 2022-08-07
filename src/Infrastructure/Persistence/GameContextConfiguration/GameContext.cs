using Domain.IContexts;
using Domain.Entities.GameEntities;
using Infrastructure.Persistence.GameContextConfiguration.Configuration;

namespace Infrastructure.Persistence.GameContextConfiguration
{
    public class GameContext : BaseContext, IGameContext
    {
        #region Properties

        public DbSet<Game> Game { get; set; } = null!;
        public DbSet<Player> Player { get; set; } = null!;

        #endregion

        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new GameConfiguration())
                        .ApplyConfiguration(new PlayerConfiguration());
        }
    }
}
