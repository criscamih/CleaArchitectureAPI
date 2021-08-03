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
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(BlogContext context) : base(context){}
        public async Task<IEnumerable<Post>> GetPostsByIdUser(int IdUser)
        {   
            return await _entity.Where(e => e.IdUser == IdUser).ToListAsync();
        }
    }
}