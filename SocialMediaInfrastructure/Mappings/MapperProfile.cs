using AutoMapper;
using SocialMediaCore.DTOs;
using SocialMediaCore.Entities;

namespace SocialMediaInfrastructure.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Post,PostDto>().ReverseMap();
            CreateMap<Comment,CommentDto>().ReverseMap();
            CreateMap<User,UserDto>().ReverseMap();
        }
    }
}