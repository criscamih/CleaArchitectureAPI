using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMediaApi.Responses;
using SocialMediaCore.DTOs;
using SocialMediaCore.Entities;
using SocialMediaCore.Interfaces;
using SocialMediaCore.QueryFilters;

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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Get([FromQuery] PostQueryFilter postFilter)
        {
            var posts =  _PostService.GetPosts(postFilter);
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            var response = new Response<IEnumerable<PostDto>>(postsDto);

            var metadata = new
            {
                posts.CurrentPage,
                posts.PageSize,
                posts.TotalPages,
                posts.Count
            };

            Response.Headers.Add("x-pagination", JsonConvert.SerializeObject(metadata));

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
            post.Id = id;
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