using System;

namespace SocialMediaCore.DTOs
{
    public class UserDto
    {
        public int IdUser { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public string Phone { get; set; }
        public bool? Active { get; set; }
    }
}