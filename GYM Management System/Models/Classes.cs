using System.ComponentModel.DataAnnotations.Schema;

namespace GYM_Management_System.Models
{
    public class Classes
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }    
        public string ClassType { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Duration { get; set; }
        public ICollection<Members> Members { get; set; } = new HashSet<Members>();
        [ForeignKey("Trainers")]
        public int TrainersId { get; set; }
        public Trainers Trainers { get; set; }


    }
}
