using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMediaCore.Entities;

namespace SocialMediaCore.Interfaces
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByIdUser(int IdUser);

    }
}