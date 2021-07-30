using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApi.Responses;
using SocialMediaCore.DTOs;
using SocialMediaCore.Entities;
using SocialMediaCore.Interfaces;

namespace SocialMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _PostService;
        private readonly IMapper _mapper;
        public PostController(IPostService PostService, IMapper mapper)
        {
            _PostService = PostService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts =  await _PostService.GetPosts();
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            var response = new Response<IEnumerable<PostDto>>(postsDto);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post =  await _PostService.GetPost(id);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new Response<PostDto>(postDto);
            return Ok(response);
        }   
                                                
        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            await _PostService.InsertPost(post);
            var result = _mapper.Map<PostDto>(post);
            var response = new Response<PostDto>(result);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Post(int id, PostDto postdto)
        {
            var post = _mapper.Map<Post>(postdto);
            post.IdPost = id;
            var result = await _PostService.UpdatePost(post);
            var psdto = _mapper.Map<PostDto>(post);
            var response = new Response<PostDto>(psdto);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Post(int id)
        {
            var result = await _PostService.DeletePost(id);
            var response = new Response<bool>(result);
            return Ok(response);
        }
    }
}