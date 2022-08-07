using Domain.Entities.BoardEntities;

namespace Domain.IRepositories
{
    public interface IBoardRepository
    {
        IQueryable<Board> Get(int id);
    }
}
