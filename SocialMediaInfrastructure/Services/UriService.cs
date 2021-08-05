using System;
using SocialMediaCore.QueryFilters;
using SocialMediaInfrastructure.Interfaces;

namespace SocialMediaInfrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUrl;
        public UriService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        public Uri GetPostPaginatorUrl(PostQueryFilter filter, string actionUrl)
        {
            var baseRoute = $"{_baseUrl}{actionUrl}";
            return new Uri(baseRoute);
        }
    }
}