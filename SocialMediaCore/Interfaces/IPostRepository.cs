using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMediaCore.Entities;

namespace SocialMediaCore.Interfaces
{
   public interface IPostRepository
   {
      Task<IEnumerable<Post>> GetPosts();
      Task<Post> GetPost(int idPost);

   }
}