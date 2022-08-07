using Domain.Entities.BoardEntities;
using Domain.IContexts;
using Domain.IRepositories;

namespace Infrastructure.Persistence.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        #region Properties

        private readonly IBoardContext _context;

        #endregion

        public BoardRepository(IBoardContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Board> Get(int id)
            => _context.Board.Where(g => g.Id == id).AsNoTracking();
    }
}
