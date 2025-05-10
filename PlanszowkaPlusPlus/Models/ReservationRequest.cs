namespace PlanszowkaPlusPlus.Models
{
    public class ReservationRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public TimeOnly TimeStart { get; set; }
        public TimeOnly TimeEnd { get; set; }
        public int TableId { get; set; }
    }
}
