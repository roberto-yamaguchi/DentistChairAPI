using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ChairService : IChairService
    {
        private readonly IChairRepository _chairRepository;

        public ChairService(IChairRepository chairRepository) {
            _chairRepository = chairRepository;
        }

        public async Task<IEnumerable<Chair>> GetAllChairsAsync() {
            return await _chairRepository.GetAllAsync();
        }

        public async Task<Chair> GetChairByIdAsync(int id) {
            return await _chairRepository.GetByIdAsync(id);
        }

        public async Task AddChairAsync(Chair chair) {
            await _chairRepository.AddAsync(chair);
        }

        public async Task UpdateChairAsync(Chair chair) {
            await _chairRepository.UpdateAsync(chair);
        }

        public async Task DeleteChairAsync(int id) {
            await _chairRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Chair>> GetAvailableChairs(DateTime startTime, DateTime endTime) {
            // Implementação básica para obter cadeiras disponíveis
            // Este método depende da implementação específica do _chairRepository
            return await _chairRepository.GetAvailableChairs(startTime, endTime);
        }

        public async Task UpdateLastUsedTime(int chairId, DateTime lastUsedTime) {
            // Este método depende da implementação específica do _chairRepository
            await _chairRepository.UpdateLastUsedTime(chairId, lastUsedTime);
        }

        public async Task AllocateChairsAutomatically(DateTime startTime, DateTime endTime) {
            // Obtém todas as cadeiras disponíveis no intervalo de tempo especificado
            var availableChairs = await _chairRepository.GetAvailableChairs(startTime, endTime);

            // Calcula o número de cadeiras disponíveis
            int totalAvailableChairs = availableChairs.Count();

            if (totalAvailableChairs == 0) {
                // Não há cadeiras disponíveis, lançar uma exceção ou lidar com isso conforme necessário
                return;
            }

            // Calcula a duração total da alocação em horas
            double totalHours = (endTime - startTime).TotalHours;

            // Calcula o número de alocações por cadeira de forma proporcional
            int allocationsPerChair = (int)Math.Ceiling(totalHours / totalAvailableChairs);

            // Distribui as alocações entre as cadeiras disponíveis
            int currentAllocationIndex = 0;
            foreach (var chair in availableChairs) {
                // Calcula os tempos de início e fim para esta alocação
                DateTime allocationStartTime = startTime.AddHours(currentAllocationIndex * allocationsPerChair);
                DateTime allocationEndTime = allocationStartTime.AddHours(allocationsPerChair);

                // Atualiza o tempo da última utilização da cadeira
                await _chairRepository.UpdateLastUsedTime(chair.Id, allocationEndTime);

                // Cria uma nova alocação de cadeira
                var allocation = new ChairAllocation {
                    ChairId = chair.Id,
                    StartTime = allocationStartTime,
                    EndTime = allocationEndTime
                };

                // Implemente o código para salvar a alocação no banco de dados
                // Se você estiver usando uma abordagem assíncrona, use await aqui
                // Exemplo: await _allocationRepository.Create(allocation);

                currentAllocationIndex++;
            }
        }
    }
}
