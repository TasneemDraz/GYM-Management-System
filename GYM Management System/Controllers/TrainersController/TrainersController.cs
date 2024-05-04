using GYM_Management_System.Context;
using GYM_Management_System.DTOS.Members;
using GYM_Management_System.DTOS.Trainers;
using GYM_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GYM_Management_System.Controllers.TrainersController
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TrainersController(ApplicationContext context)
        {
            _context=context;
        }

        [HttpGet]
        public IActionResult GetAllTrainers()
        {
            List<TrainerDto> trainerDtos = _context.Trainers
            .Include(t => t.Members)
                .Include(t => t.Classes)
                .Select(t => new TrainerDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Email = t.Email,
                    Address = t.Address,
                    PhoneNumber = t.PhoneNumber,
                    Gender = t.Gender,
                    DateOfBirth = t.DateOfBirth,
                   DateofHire = t.DateofHire,
                   Salary = t.Salary,
                   MembersName=t.Members.Select(m=>m.Name).ToList(),
                    Classes = t.Classes.Select(c => c.ClassName).ToList()
                })
                .ToList();

            return Ok(trainerDtos);
        }
        [HttpGet("{id}")]
        public IActionResult GetTrainerById(int id)
        {
            var trainer = _context.Trainers
                .Include(m => m.Members)
                .Include(m => m.Classes)
                .FirstOrDefault(t => t.Id == id);

            if (trainer == null)
            {
                return NotFound();
            }

            var trainerDto = new TrainerDto
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Email = trainer.Email,
                Address = trainer.Address,
                PhoneNumber = trainer.PhoneNumber,
                Gender = trainer.Gender,
                DateOfBirth = trainer.DateOfBirth,
                DateofHire = trainer.DateofHire,
                Salary = trainer.Salary,
                MembersName = trainer.Members.Select(m => m.Name).ToList(),
                Classes = trainer.Classes.Select(c => c.ClassName).ToList()
            };

            return Ok(trainerDto);
        }



        [HttpGet("{name}")]
        public IActionResult GetTrainerByName(string name)
        {
            var trainer = _context.Trainers
                .Include(m => m.Members)
                .Include(m => m.Classes)
                .FirstOrDefault(t => t.Name == name);

            if (trainer == null)
            {
                return NotFound();
            }

            var trainerDto = new TrainerDto
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Email = trainer.Email,
                Address = trainer.Address,
                PhoneNumber = trainer.PhoneNumber,
                Gender = trainer.Gender,
                DateOfBirth = trainer.DateOfBirth,
                DateofHire = trainer.DateofHire,
                Salary = trainer.Salary,
                MembersName = trainer.Members.Select(m => m.Name).ToList(),
                Classes = trainer.Classes.Select(c => c.ClassName).ToList()
            };

            return Ok(trainerDto);
        }



        [HttpPost]
        public IActionResult AddNewTrainer(AddNewTrainerDto NewTrainer)
        {
            var member = _context.Members.Where(m=> NewTrainer.MembersName.Contains(m.Name)).ToList();
            if (member == null)
            {
                return BadRequest("One or more Members not found");
            }
            var classes = _context.Classes.Where(c => NewTrainer.Classes.Contains(c.ClassName)).ToList();
            if (classes.Count != NewTrainer.Classes.Count)
            {
                return BadRequest("One or more classes not found");
            }

            var trainer = new Trainers
            {
                Name = NewTrainer.Name,
                Email = NewTrainer.Email,
                Address = NewTrainer.Address,
                PhoneNumber = NewTrainer.PhoneNumber,
                Gender = NewTrainer.Gender,
                Specialization = NewTrainer.Specialization,
                Salary=NewTrainer.Salary,
                DateOfBirth = NewTrainer.DateOfBirth,
                DateofHire = NewTrainer.DateofHire,
                 Members=member,
                Classes = classes
            };
            _context.Trainers.Add(trainer);
            _context.SaveChanges();
            return Ok("New Trainer added successfully");
        }



        [HttpPut("{id}")]
        public IActionResult UpdateTrainer(int id, UpdateTrainerDto updatedTrainerDto)
        {
            var trainer = _context.Trainers.FirstOrDefault(t => t.Id == id);

            if (trainer == null)
            {
                return NotFound();
            }

            trainer.Name = updatedTrainerDto.Name;
            trainer.Email = updatedTrainerDto.Email;
            trainer.Address = updatedTrainerDto.Address;
            trainer.PhoneNumber = updatedTrainerDto.PhoneNumber;
            trainer.Gender = updatedTrainerDto.Gender;
            trainer.DateOfBirth = updatedTrainerDto.DateOfBirth;
           trainer.DateofHire = updatedTrainerDto.DateofHire;
            trainer.Specialization= updatedTrainerDto.Specialization;
            trainer.Salary = updatedTrainerDto.Salary;  

            _context.SaveChanges();

            return Ok("Trainer updated successfully");
        }

    
        [HttpDelete("{id}")]
        public IActionResult DeleteTrainer(int id)
        {
            var trainer = _context.Trainers
                .Include(t => t.Members)
                .Include(t => t.Classes)
                .FirstOrDefault(t => t.Id == id);

            if (trainer == null)
            {
                return NotFound();
            }

            // Remove all associated members
            _context.Members.RemoveRange(trainer.Members);

            // Remove all associated classes
            _context.Classes.RemoveRange(trainer.Classes);
            _context.Trainers.Remove(trainer);
            _context.SaveChanges();

            return Ok("Trainer and associated data deleted successfully");
        }



    }
}
