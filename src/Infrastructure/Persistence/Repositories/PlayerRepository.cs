using Domain.IContexts;
using Domain.Entities.GameEntities;
using Domain.IRepositories;

namespace Infrastructure.Persistence.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        #region Properties

        private readonly IGameContext _context;

        #endregion

        public PlayerRepository(IGameContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Player?> Find(int id)
            => await _context.Player.FindAsync(id);

        public async Task Update(Player? item)
        {
            if (item != null)
            {
                _context.Player.Update(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
