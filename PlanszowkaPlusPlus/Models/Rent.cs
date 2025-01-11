using System.ComponentModel.DataAnnotations.Schema;

namespace PlanszowkaPlusPlus.Models
{
    public class Rent
    {
        public int Id { get; set; }
        public DateOnly RentDate { get; set; }
        public DateOnly? ReturnDate { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        //relation properties
        //public Game Game { get; set; }
        //public Member Member { get; set; }

    }
}