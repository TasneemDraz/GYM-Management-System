namespace GYM_Management_System.DTOS.Trainers
{
    public class UpdateTrainerDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateofHire { get; set; }
        public string Specialization { get; set; }
        public Decimal Salary { get; set; }
    }
}
