using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialMediaCore.Entities;
using SocialMediaCore.Interfaces;

namespace SocialMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts =  await _postRepository.GetPosts();
            return Ok(posts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post =  await _postRepository.GetPost(id);
            return Ok(post);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            await _postRepository.InsertPost(post);
            return Ok(post);
        }
    }
}