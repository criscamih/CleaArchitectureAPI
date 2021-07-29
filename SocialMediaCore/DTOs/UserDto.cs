using System;

namespace SocialMediaCore.DTOs
{
    public class UserDto
    {
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public string Phone { get; set; }
        public bool? Active { get; set; }
    }
}