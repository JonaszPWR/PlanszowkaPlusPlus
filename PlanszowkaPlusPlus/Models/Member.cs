namespace PlanszowkaPlusPlus.Models {

    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;//hashing or encryption?
        public DateTime RegistrationDate { get; set; }
        public string Interests { get; set; } = string.Empty;
        
        public string PasswordHash{ get; set; } = string.Empty;

    }
}