using System;
using System.Threading.Tasks;
using SocialMediaCore.Entities;

namespace SocialMediaCore.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository postRespository { get; }
        IBaseRepository<User> userRespository { get; }
        IBaseRepository<Comment> commentRespository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}