﻿namespace GYM_Management_System.DTOS.Members
{
    public class UpdateMemberDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }


    }
}
