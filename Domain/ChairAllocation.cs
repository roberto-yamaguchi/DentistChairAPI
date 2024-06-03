namespace Domain.Entities
{
    public class ChairAllocation
    {
        public int Id { get; set; }
        public int ChairId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
