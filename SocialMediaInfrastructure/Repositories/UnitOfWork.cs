using System;
using System.Threading.Tasks;
using SocialMediaCore.Entities;
using SocialMediaCore.Interfaces;
using SocialMediaInfrastructure.Data;

namespace SocialMediaInfrastructure.Repositories 
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogContext _context;
        // private readonly IBaseRepository<Post> _postRepository;
        // private readonly IBaseRepository<User> _userRepository;
        // private readonly IBaseRepository<Comment> _commentRepository;

        public UnitOfWork(BlogContext context)
        {
            _context = context;
        }
        public IPostRepository postRespository => new PostRepository(_context);

        public IBaseRepository<User> userRespository => new BaseRepository<User>(_context);

        public IBaseRepository<Comment> commentRespository => new BaseRepository<Comment>(_context);

        public void Dispose()
        {
            if(_context != null)
                _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}