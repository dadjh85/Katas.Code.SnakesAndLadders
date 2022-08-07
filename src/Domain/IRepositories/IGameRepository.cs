using Domain.Entities.GameEntities;

namespace Domain.IRepositories
{
    public interface IGameRepository
    {
        Task<Game> Add(Game item);
        IQueryable<Game> Get(int id);
        Task Update(Game? item);
        Task<Game?> Find(int id);
    }
}
