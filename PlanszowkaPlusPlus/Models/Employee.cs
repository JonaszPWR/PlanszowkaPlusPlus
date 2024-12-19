namespace PlanszowkaPlusPlus.Models {
    
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }//hashing or encryption?
        public string Password { get; set; }//hashing or encryption?

    }
}