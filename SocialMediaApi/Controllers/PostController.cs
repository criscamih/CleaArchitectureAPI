using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMediaApi.Responses;
using SocialMediaCore.CustomEntities;
using SocialMediaCore.DTOs;
using SocialMediaCore.Entities;
using SocialMediaCore.Interfaces;
using SocialMediaCore.QueryFilters;
using SocialMediaInfrastructure.Interfaces;

namespace SocialMediaApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _PostService;
        private readonly IMapper _mapper;

        private readonly IUriService _uriService;
        public PostController(IPostService PostService, IMapper mapper, IUriService uriService)
        {
            _PostService = PostService;
            _mapper = mapper;
            _uriService = uriService;
        }

        ///<summary>
        /// Allows you to get all of the posts or filter by some attributes
        ///</summary>
        ///<param name="filters">Filter Search</param>
        ///<returns>all posts</returns>
        [HttpGet(Name = nameof(Get))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type= typeof(Response<IEnumerable<PostDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Get([FromQuery] PostQueryFilter filters)
        {
            var posts =  _PostService.GetPosts(filters);
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            var metadata = new PageMetadata
            {
                CurrentPage = posts.CurrentPage,
                PageSize = posts.PageSize,
                TotalPages = posts.TotalPages,
                TotalCount = posts.TotalCount,
                PreviousPageNumber = posts.PreviousPageNumber,
                NextPageNumber = posts.NextPageNumber,
                HasNextPage = posts.HasNextPage,
                HasPreviousPage = posts.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginatorUrl(filters,Url.RouteUrl(nameof(Get))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginatorUrl(filters,Url.RouteUrl(nameof(Get))).ToString()
            };

            var response = new Response<IEnumerable<PostDto>>(postsDto)
            {
                meta = metadata
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