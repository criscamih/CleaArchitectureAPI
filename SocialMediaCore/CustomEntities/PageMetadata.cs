using System;

namespace SocialMediaCore.CustomEntities
{
    public class PageMetadata
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int? NextPageNumber { get; set;}
        public int? PreviousPageNumber { get; set; }
        public string NextPageUrl { get; set;}
        public string PreviousPageUrl { get; set; }


    }
}