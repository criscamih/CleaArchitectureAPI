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
        public ActionResult Get()
        {
            var posts =  _postRepository.GetPosts();
            return Ok(posts);
        }
    }
}