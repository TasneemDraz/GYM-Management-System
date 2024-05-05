using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM_Management_System.Models
{
    public class Members
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [StringLength(500)]
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
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpirDate { get; set; }
        // Navigational property 
        [ForeignKey("Trainers")]
        [Required]

        public int TrainersId { get; set; }
        public Trainers Trainers { get; set; }
     
        public ICollection<Classes> Classes { get; set; } = new HashSet<Classes>();




        // each member has one trainer and each trainer has many members 1 to many
        // each member has many classes and each class has many members many to many 
        //each trainer has many classes and each class has one trainer 1 to many 
    }
}
