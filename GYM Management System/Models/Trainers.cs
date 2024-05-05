using System.ComponentModel.DataAnnotations;

namespace GYM_Management_System.Models
{
    public class Trainers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [StringLength(500)]
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(255)]
        public string Gender { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateofHire { get; set; }
        [Required]
        [StringLength(255)]
        public string Specialization { get; set; }
        [Required]
        public Decimal Salary { get; set; }
        public ICollection<Members> Members { get; set; }= new HashSet<Members>();
        public ICollection<Classes> Classes { get; set; } = new HashSet<Classes>();



    }
}
