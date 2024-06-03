using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ChairRepository : IChairRepository
    {
        private readonly ApplicationDbContext _context;

        public ChairRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Chair>> GetAllAsync() {
            return await _context.Chairs.ToListAsync();
        }

        public async Task<Chair> GetByIdAsync(int id) {
            return await _context.Chairs.FindAsync(id);
        }

        public async Task AddAsync(Chair chair) {
            await _context.Chairs.AddAsync(chair);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Chair chair) {
            _context.Chairs.Update(chair);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            var chair = await _context.Chairs.FindAsync(id);
            if (chair != null) {
                _context.Chairs.Remove(chair);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Chair>> GetAvailableChairs(DateTime startTime, DateTime endTime) {
            // Implementação básica para obter cadeiras disponíveis
            return await _context.Chairs
                .Where(c => c.LastUsedTime <= startTime || c.LastUsedTime <= endTime)
                .ToListAsync();
        }

        public async Task UpdateLastUsedTime(int chairId, DateTime lastUsedTime) {
            var chair = await _context.Chairs.FindAsync(chairId);

            if (chair != null) {
                chair.LastUsedTime = lastUsedTime;
                await _context.SaveChangesAsync();
            } else {
                throw new Exception("Cadeira não encontrada.");
            }
        }
    }
}
