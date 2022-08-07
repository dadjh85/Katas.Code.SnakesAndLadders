using Domain.Entities.GameEntities;

namespace Domain.IRepositories
{
    public interface IPlayerRepository
    {
        Task<Player?> Find(int id);
        Task Update(Player? item);
    }
}
