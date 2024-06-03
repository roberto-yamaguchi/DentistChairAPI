using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IChairRepository
    {
        Task<IEnumerable<Chair>> GetAllAsync();
        Task<Chair> GetByIdAsync(int id);
        Task AddAsync(Chair chair);
        Task UpdateAsync(Chair chair);
        Task DeleteAsync(int id);
        Task UpdateLastUsedTime(int chairId, DateTime lastUsedTime);
        Task<IEnumerable<Chair>> GetAvailableChairs(DateTime startTime, DateTime endTime);
    }
}
