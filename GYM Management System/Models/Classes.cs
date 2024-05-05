using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM_Management_System.Models
{
    public class Classes
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string ClassName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public int Capacity { get; set; }
        [StringLength(255)]
        public string ClassType { get; set; }
        [Required]
        [DataType(DataType.DateTime)]

        public DateTime DateTime { get; set; }
        [Required]
        public decimal Duration { get; set; }
        public ICollection<Members> Members { get; set; } = new HashSet<Members>();
        [ForeignKey("Trainers")]
        [Required]
        public int TrainersId { get; set; }
        public Trainers Trainers { get; set; }


    }
}
