using System;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaCore.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}