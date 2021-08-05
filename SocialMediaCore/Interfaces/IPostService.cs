using System.Threading.Tasks;
using SocialMediaCore.CustomEntities;
using SocialMediaCore.Entities;
using SocialMediaCore.QueryFilters;

namespace SocialMediaCore.Interfaces
{
   public interface IPostService
   {
      PagedList<Post> GetPosts(PostQueryFilter postFilter);
      Task<Post> GetPost(int idPost);
      Task InsertPost(Post post);
      Task<bool> UpdatePost(Post post);
      Task<bool> DeletePost(int id);

   }
}