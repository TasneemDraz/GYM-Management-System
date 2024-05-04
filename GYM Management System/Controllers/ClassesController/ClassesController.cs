using GYM_Management_System.Context;
using GYM_Management_System.DTOS.Classes;
using GYM_Management_System.DTOS.Members;
using GYM_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GYM_Management_System.Controllers.ClassesController
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ClassesController(ApplicationContext context)
        {
            _context=context;
        }


        [HttpGet]
        public IActionResult GetAllClasses()
        {
            List<ClassesDto> classesDto = _context.Classes
            .Include(c => c.Trainers)
                .Include(c => c.Members)
                .Select(c => new ClassesDto
                {
                    Id = c.Id,
                   ClassName = c.ClassName,
                   Description= c.Description,
                   Capacity= c.Capacity,
                   ClassType = c.ClassType,
                   DateTime = c.DateTime,
                   Duration = c.Duration,
                   MemberName=c.Members.Select(m => m.Name).ToList(),
                   TrainerName=c.Trainers.Name,
                })
                .ToList();

            return Ok(classesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetClassesById(int id)
        {
            var classes = _context.Classes
                .Include(c => c.Trainers)
                .Include(c => c.Members)
                .FirstOrDefault(c => c.Id == id);

            if (classes == null)
            {
                return NotFound();
            }

            var classesDto = new ClassesDto
            {
                Id= classes.Id,
                ClassName = classes.ClassName,
                Description= classes.Description,
                Capacity= classes.Capacity,
                ClassType = classes.ClassType,
                DateTime = classes.DateTime,
                Duration = classes.Duration,
                MemberName=classes.Members.Select(c=>c.Name).ToList(),
                TrainerName = classes.Trainers.Name,

            };

            return Ok(classesDto);
        }

        [HttpGet("{name}")]
        public IActionResult GetClassesByName(string name)
        {
            var classes = _context.Classes
                .Include(c => c.Trainers)
                .Include(c => c.Members)
                .FirstOrDefault(c => c.ClassName == name);

            if (classes == null)
            {
                return NotFound();
            }

            var classesDto = new ClassesDto
            {
                Id = classes.Id,
                ClassName = classes.ClassName,
                Description = classes.Description,
                Capacity = classes.Capacity,
                ClassType = classes.ClassType,
                DateTime = classes.DateTime,
                Duration = classes.Duration,
                MemberName = classes.Members.Select(m => m.Name).ToList(),
                TrainerName = classes.Trainers?.Name, // Ensure Trainers is not null before accessing its properties
            };

            return Ok(classesDto);
        }



        [HttpPost]
        public IActionResult AddNewClasses(AddNewClassesDto newClass)
        {
            var trainer = _context.Trainers.FirstOrDefault(t => t.Name == newClass.TrainerName);
            if (trainer == null)
            {
                return BadRequest("Trainer not found");
            }

            var members = _context.Members.Where(m => newClass.MemberName.Contains(m.Name)).ToList();
            if (members.Count != newClass.MemberName.Count)
            {
                return BadRequest("One or more members not found");
            }

            var classes = new Classes
            {
                ClassName = newClass.ClassName,
                Description = newClass.Description,
                Capacity = newClass.Capacity,
                ClassType = newClass.ClassType,
                DateTime = newClass.DateTime,
                Duration = newClass.Duration,
                Trainers = trainer,     
                Members = members       
            };

            _context.Classes.Add(classes);
            _context.SaveChanges();

            return Ok("New class added successfully");
        }


        [HttpPut("{id}")]
        public IActionResult UpdateMember(int id, UpdateClassesDto updatedClassesDto)
        {
            var classes = _context.Classes.FirstOrDefault(m => m.Id == id);

            if (classes == null)
            {
                return NotFound();
            }
            classes.ClassName = updatedClassesDto.ClassName;
            classes.Duration = updatedClassesDto.Duration;
            classes.ClassType = updatedClassesDto.ClassType;
            classes.DateTime = updatedClassesDto.DateTime;
            classes.Description = updatedClassesDto.Description;
            classes.Capacity = updatedClassesDto.Capacity;



            _context.SaveChanges();

            return Ok("class updated successfully");
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            var classes = _context.Classes.FirstOrDefault(m => m.Id == id);

            if (classes == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(classes);

            _context.SaveChanges();

            return Ok("Classes deleted successfully");
        }
    }
}
