using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}