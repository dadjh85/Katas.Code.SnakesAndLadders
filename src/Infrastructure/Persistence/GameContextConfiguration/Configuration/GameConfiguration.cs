using Domain.Entities.GameEntities;

namespace Infrastructure.Persistence.GameContextConfiguration.Configuration
{
    internal class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(entity => entity.StartDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(entity => entity.IsFinished)
                   .HasDefaultValue(false);

            builder.HasQueryFilter(filter => !filter.IsFinished);
        }
    }
}
