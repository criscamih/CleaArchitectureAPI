using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMediaCore.DTOs;
using SocialMediaCore.Entities;
using SocialMediaCore.Interfaces;

namespace SocialMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts =  await _postRepository.GetPosts();
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            return Ok(posts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post =  await _postRepository.GetPost(id);
            var postDto = _mapper.Map<PostDto>(post);
            return Ok(post);
        }                                           
        [HttpPost]
        public async Task<IActionResult> Post(PostDto post)
        {
            var postd = _mapper.Map<Post>(post);
            await _postRepository.InsertPost(postd);
            return Ok(postd);
        }
    }
}