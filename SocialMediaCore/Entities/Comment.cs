using System;
using System.Collections.Generic;

namespace SocialMediaCore.Entities
{
    public partial class Comment
    {
        public int IdComment { get; set; }
        public int IdPost { get; set; }
        public int IdUser { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool? Active { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
