using System;

namespace SocialMediaCore.QueryFilters
{
    public class PostQueryFilter
    {

        public int? IdUser { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        // public PostQueryFilter()
        // {
        //     PageNumber = PageNumber??1;
        //     PageSize = PageSize??10;
        // }
    }
}