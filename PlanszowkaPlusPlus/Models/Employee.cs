namespace PlanszowkaPlusPlus.Models {
    
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty;//hashing or encryption?
        public string PasswordHash{ get; set; } = string.Empty;//hashing or encryption?

    }
}