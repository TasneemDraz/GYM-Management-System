namespace GYM_Management_System.DTOS.Classes
{
    public class ClassesDto
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string ClassType { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Duration { get; set; }
        public string TrainerName { get; set; }
        public List<string> MemberName { get; set; }
    }
}
