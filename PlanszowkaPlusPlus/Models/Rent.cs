namespace PlanszowkaPlusPlus.Models
{
    public class Rent
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int MemberId { get; set; }
        public DateOnly RentDate { get; set; }

        public DateOnly? ReturnDate { get; set; }

    }
}