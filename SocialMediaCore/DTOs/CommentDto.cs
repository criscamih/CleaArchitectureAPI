using System;

namespace SocialMediaCore.DTOs
{
    public class CommentDto
    {
        public int IdPost { get; set; }
        public int IdUser { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool? Active { get; set; }
    }
}