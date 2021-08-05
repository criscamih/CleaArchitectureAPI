using System;
using SocialMediaCore.QueryFilters;

namespace SocialMediaInfrastructure.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginatorUrl(PostQueryFilter filter, string actionUrl);
    }
}