using Domain.Entities.BoardEntities;
using Domain.IContexts;
using Infrastructure.Persistence.BoardContextConfiguration.Configuration;

namespace Infrastructure.Persistence.BoardContextConfiguration
{
    public class BoardContext : BaseContext, IBoardContext
    {
        #region Properties

        public DbSet<Board> Board { get; set; } = null!;
        public DbSet<SnakeAndLader> SnakeAndLader { get; set; } = null!;

        #endregion

        public BoardContext(DbContextOptions<BoardContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BoardConfiguration())
                        .ApplyConfiguration(new SnakeAndLaderConfiguration());
        }
    }
}
