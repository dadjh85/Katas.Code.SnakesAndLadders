using Domain.IContexts;
using Domain.Entities.GameEntities;
using Domain.IRepositories;

namespace Infrastructure.Persistence.Repositories
{
    public class GameRepository : IGameRepository
    {
        #region Properties

        private readonly IGameContext _context;

        #endregion

        public GameRepository(IGameContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Game> Add(Game item)
        {
            _context.Game.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public IQueryable<Game> Get(int id)
            => _context.Game.Where(g => g.Id == id).AsNoTracking();

        public async Task<Game?> Find(int id)
            => await _context.Game.FindAsync(id);

        public async Task Update(Game? item)
        {
            if (item != null)
            {
                _context.Game.Update(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
