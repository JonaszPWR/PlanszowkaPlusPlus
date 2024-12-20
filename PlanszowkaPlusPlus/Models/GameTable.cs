namespace PlanszowkaPlusPlus.Models
{
    public class GameTable
    {
        //public Table() { generateId(); Number...; isFree = true; }
        public int Id { get; set; }
        public int Number { get; set; }
        public bool IsFree { get; set; }
        //relation properties
        //public ICollection<Reservation>? Reservations { get; set; }
    }

}
