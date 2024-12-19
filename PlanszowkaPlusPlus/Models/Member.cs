namespace PlanszowkaPlusPlus.Models {
    
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }//hashing or encryption?
        public DateTime RegistrationDate { get; set; }
        //navigation properties
        public ICollection<Rent>? Rent { get; set; }//when updating a Member, if(!member.Rent) member.Rent =  new List<Rent>;
    }
}