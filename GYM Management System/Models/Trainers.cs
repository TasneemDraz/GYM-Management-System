namespace GYM_Management_System.Models
{
    public class Trainers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateofHire { get; set; }
        public string Specialization { get; set; }
        public Decimal Salary { get; set; }
        public ICollection<Members> Members { get; set; }= new HashSet<Members>();
        public ICollection<Classes> Classes { get; set; } = new HashSet<Classes>();



    }
}
