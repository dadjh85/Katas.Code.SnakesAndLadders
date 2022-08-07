using Domain.Entities.BoardEntities;

namespace Infrastructure.Persistence.BoardContextConfiguration.Configuration
{
    internal class BoardConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.Property(entity => entity.Id)
                   .ValueGeneratedNever();

            builder.HasMany(entity => entity.SnakesAndLaders)
                   .WithMany(entity => entity.Boards)
                   .UsingEntity(entity => entity.ToTable("BoardsSnakesAndLaders"));
        }
    }
}
