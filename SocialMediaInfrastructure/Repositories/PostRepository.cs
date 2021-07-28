using System;
using System.Linq;
using SocialMediaCore.Entities;
using System.Collections.Generic;
using SocialMediaCore.Interfaces;
using System.Threading.Tasks;
using SocialMediaInfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaInfrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogContext _context;
        public PostRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Publicacion>> GetPosts()
        {
            try
            {
                var posts = _context.Publicacion.ToList();
                return await Task.FromResult(posts);
            }
            catch (System.Exception ex)
            {
                
                throw;
            }
        }
    }
}