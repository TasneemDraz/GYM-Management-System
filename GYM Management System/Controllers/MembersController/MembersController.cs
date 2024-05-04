using GYM_Management_System.Context;
using GYM_Management_System.DTOS.Members;
using GYM_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GYM_Management_System.Controllers.MembersController
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class MembersController : ControllerBase
    {

        private readonly ApplicationContext _context;

        public MembersController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetAllMembers()
        {
            List<MemberDto> memberDTOs = _context.Members
            .Include(m => m.Trainers)
                .Include(m => m.Classes)
                .Select(m => new MemberDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Email = m.Email,
                    Address = m.Address,
                    PhoneNumber = m.PhoneNumber,
                    Gender = m.Gender,
                    DateOfBirth = m.DateOfBirth,
                    StartDate = m.StartDate,
                    ExpiryDate = m.ExpirDate,
                    TrainerName = m.Trainers.Name,
                    Classes = m.Classes.Select(c => c.ClassName).ToList()
                })
                .ToList();

            return Ok(memberDTOs);
        }
        [HttpGet("{id}")]
        public IActionResult GetMemberById(int id)
        {
            var member = _context.Members
                .Include(m => m.Trainers)
                .Include(m => m.Classes)
                .FirstOrDefault(m => m.Id == id);

            if (member == null)
            {
                return NotFound(); 
            }

            var memberDto = new MemberDto
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Address = member.Address,
                PhoneNumber = member.PhoneNumber,
                Gender = member.Gender,
                DateOfBirth = member.DateOfBirth,
                StartDate = member.StartDate,
                ExpiryDate = member.ExpirDate,
                TrainerName = member.Trainers.Name,
                Classes = member.Classes.Select(c => c.ClassName).ToList()
            };

            return Ok(memberDto); 
        }

        [HttpGet("{name}")]
        public IActionResult GetMemberByName(string name)
        {
            var member = _context.Members
                .Include(m => m.Trainers)
                .Include(m => m.Classes)
                .FirstOrDefault(m => m.Name == name);

            if (member == null)
            {
                return NotFound(); 
            }

            var memberDto = new MemberDto
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Address = member.Address,
                PhoneNumber = member.PhoneNumber,
                Gender = member.Gender,
                DateOfBirth = member.DateOfBirth,
                StartDate = member.StartDate,
                ExpiryDate = member.ExpirDate,
                TrainerName = member.Trainers.Name,
                Classes = member.Classes.Select(c => c.ClassName).ToList()
            };

            return Ok(memberDto);
        }

        [HttpPost]
        public IActionResult AddNewMember(AddNewMemberDto NewMember)
        {
            var trainer = _context.Trainers.FirstOrDefault(t => t.Name == NewMember.TrainerName);
            if (trainer == null)
            {
                return BadRequest("Trainer not found");
            }
            var classes = _context.Classes.Where(c => NewMember.ClassNames.Contains(c.ClassName)).ToList();
            if (classes.Count != NewMember.ClassNames.Count)
            {
                return BadRequest("One or more classes not found");
            }

            var member = new Members
            {
                Name = NewMember.Name,
                Email = NewMember.Email,
                Address = NewMember.Address,
                PhoneNumber = NewMember.PhoneNumber,
                Gender = NewMember.Gender,
                DateOfBirth = NewMember.DateOfBirth,
                StartDate = NewMember.StartDate,
                ExpirDate = NewMember.ExpiryDate,
                Trainers = trainer,
                Classes = classes
            };
            _context.Members.Add(member);
            _context.SaveChanges();
            return Ok("New Member added successfully");
        }


        [HttpPut("{id}")]
        public IActionResult UpdateMember(int id, UpdateMemberDto updatedMemberDto)
        {
            var member = _context.Members.FirstOrDefault(m => m.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            member.Name = updatedMemberDto.Name;
            member.Email = updatedMemberDto.Email;
            member.Address = updatedMemberDto.Address;
            member.PhoneNumber = updatedMemberDto.PhoneNumber;
            member.Gender = updatedMemberDto.Gender;
            member.DateOfBirth = updatedMemberDto.DateOfBirth;
            member.StartDate = updatedMemberDto.StartDate;
            member.ExpirDate = updatedMemberDto.ExpiryDate;

            _context.SaveChanges();

            return Ok("Member updated successfully");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMemberWithTrainer(int id, UpdateMemberWithTrainerDto updatedMemberDto)
        {
            var member = _context.Members.FirstOrDefault(m => m.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            var trainer = _context.Trainers.FirstOrDefault(t => t.Id == updatedMemberDto.TrainerId);

            if (trainer == null)
            {
                return BadRequest("Trainer not found");
            }

            member.Name = updatedMemberDto.Name;
            member.Email = updatedMemberDto.Email;
            member.Address = updatedMemberDto.Address;
            member.PhoneNumber = updatedMemberDto.PhoneNumber;
            member.Gender = updatedMemberDto.Gender;
            member.DateOfBirth = updatedMemberDto.DateOfBirth;
            member.StartDate = updatedMemberDto.StartDate;
            member.ExpirDate = updatedMemberDto.ExpiryDate;
            member.TrainersId = trainer.Id;
            member.Trainers = trainer;

            _context.SaveChanges();

            return Ok("Member updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            var member = _context.Members.FirstOrDefault(m => m.Id == id);

            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);

            _context.SaveChanges();

            return Ok("Member deleted successfully");
        }


    }
}
