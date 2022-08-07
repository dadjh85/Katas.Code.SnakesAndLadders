using Domain.Entities.GameEntities;

namespace Infrastructure.Persistence.GameContextConfiguration.Configuration
{
    internal class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasOne(entity => entity.GameNavigation)
                   .WithMany(entity => entity.Players)
                   .HasForeignKey(entity => entity.GameId);

            builder.Property(entity => entity.Name)
                   .HasMaxLength(250);

        }
    }
}
