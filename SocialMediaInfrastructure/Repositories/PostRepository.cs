using System;
using System.Linq;
using SocialMediaCore.Entities;
using System.Collections.Generic;
using SocialMediaCore.Interfaces;
using System.Threading.Tasks;
using SocialMediaInfrastructure.Data;


namespace SocialMediaInfrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogContext _context;
        public PostRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            try
            {
                var posts = _context.Posts.ToList();
                return await Task.FromResult(posts);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<Post> GetPost(int idPost)
        {
            try
            {
                var post = _context.Posts.FirstOrDefault(p => p.IdPost == idPost);
                return await Task.FromResult(post);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}