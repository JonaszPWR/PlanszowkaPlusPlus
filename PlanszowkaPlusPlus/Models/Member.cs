namespace PlanszowkaPlusPlus.Models {
    
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; }
        public ICollection<Rent> Rent { get; set; }
    }
}