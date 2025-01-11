namespace PlanszowkaPlusPlus.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int TotalNumber { get; set; }
        public int AvailableNumber { get; set; }
        //relation properties
        //public ICollection<Rent>? Rented { get; set; }?
    }
}