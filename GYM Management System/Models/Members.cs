using System.ComponentModel.DataAnnotations.Schema;

namespace GYM_Management_System.Models
{
    public class Members
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirDate { get; set; }
        // Navigational property 
        [ForeignKey("Trainers")]

        public int TrainersId { get; set; }
        public Trainers Trainers { get; set; }
     
        public ICollection<Classes> Classes { get; set; } = new HashSet<Classes>();




        // each member has one trainer and each trainer has many members 1 to many
        // each member has many classes and each class has many members many to many 
        //each trainer has many classes and each class has one trainer 1 to many 
    }
}
