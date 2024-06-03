using Domain.Entities;

namespace Application.Interfaces
{
    public interface IChairService
    {
        Task<IEnumerable<Chair>> GetAllChairsAsync();
        Task<Chair> GetChairByIdAsync(int id);
        Task AddChairAsync(Chair chair);
        Task UpdateChairAsync(Chair chair);
        Task DeleteChairAsync(int id);
        //Task<Chair> AllocateChairAsync(DateTime startTime, DateTime endTime);
    }
}
