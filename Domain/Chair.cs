namespace Domain.Entities
{
    public class Chair
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public required string Description { get; set; }
        public required string Model { get; set; }
        public required string SerialNumber { get; set; }
        public bool IsAvailable { get; set; }
        public int AllocationCount { get; set; } // Contador para rastrear o número de alocações

        // Relacionamento com os compromissos (agendamentos) da cadeira
        public required List<ChairAllocation> Appointments { get; set; }
        public DateTime? LastUsedTime { get; set; }
    }
}
