namespace PlanszowkaPlusPlus.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int TotalNumber { get; set; }
        public int AvailableNumber { get; set; }
        //relation properties
        //public ICollection<Rent>? Rented { get; set; }?
    }
}