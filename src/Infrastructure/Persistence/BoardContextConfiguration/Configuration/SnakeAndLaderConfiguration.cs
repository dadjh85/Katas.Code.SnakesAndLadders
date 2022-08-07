using Domain.Entities.BoardEntities;

namespace Infrastructure.Persistence.BoardContextConfiguration.Configuration
{
    internal class SnakeAndLaderConfiguration : IEntityTypeConfiguration<SnakeAndLader>
    {
        public void Configure(EntityTypeBuilder<SnakeAndLader> builder)
        {
            builder.Property(entity => entity.Id).ValueGeneratedNever();
        }
    }
}
