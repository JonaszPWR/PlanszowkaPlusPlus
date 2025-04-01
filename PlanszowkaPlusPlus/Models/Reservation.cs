using System.ComponentModel.DataAnnotations.Schema;

namespace PlanszowkaPlusPlus.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateOnly ReservationDate { get; set; }
        public TimeOnly TimeStart { get; set; }
        public TimeOnly TimeEnd { get; set; }
        public bool IsArchived { get; set; } = false;

        [ForeignKey("GameTable")]
        public int TableId { get; set; }
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        //relation properties
        public GameTable GameTable { get; set; }
        public Member Member { get; set; }
    }
    public class ReservationDTO
    {
        public int Id { get; set; }
        public DateOnly ReservationDate { get; set; }
        public TimeOnly TimeStart { get; set; }
        public TimeOnly TimeEnd { get; set; }
        public int TableId { get; set; }
        public int MemberId { get; set; }
    }
}