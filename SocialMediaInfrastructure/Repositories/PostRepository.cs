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
        public async Task InsertPost(Post post)
        {
            try
            {
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task<bool> UpdatePost(Post post)
        {
            try
            {
                var postCurrent = await GetPost(post.IdPost);
                postCurrent.IdUser = post.IdUser;
                postCurrent.Description = post.Description;
                postCurrent.Date = post.Date;
                postCurrent.Image = post.Image;

                int rows = await _context.SaveChangesAsync();
                return rows > 0;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> DeletePost(int id)
        {
            try
            {
                var postCurrent = await GetPost(id);
                _context.Remove(postCurrent);
                int rows = await _context.SaveChangesAsync();
                return rows>0;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}