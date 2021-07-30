using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialMediaCore.Entities;
using SocialMediaCore.Interfaces;

namespace SocialMediaCore.Services
{
   public class PostService : IPostService
   {
      private readonly IPostRepository _repository;
      private readonly IUserRepository _userRepository;

      public PostService(IPostRepository repository,IUserRepository userRepository)
      {
          _repository = repository;
          _userRepository = userRepository;
      }

      public async Task<IEnumerable<Post>> GetPosts()
      {
         var posts = await _repository.GetPosts();
         return posts;
      }

      public async Task<Post> GetPost(int idPost)
      {
         var post = await _repository.GetPost(idPost);
         return post;
      }

      public async Task InsertPost(Post post)
      {
         // validar si el usuario existe en la base de datos.
         var user = await _userRepository.GetUser(post.IdUser);
         if (user is null)
            throw new Exception("User Doesn't exist");
         // validar que no se pueda usar la palabra sexo.
         if (post.Description.Contains("sexo"))
            throw new Exception("Content not allowed");
         await _repository.InsertPost(post);
      }

      public async Task<bool> UpdatePost(Post post)
      {
         var result = await _repository.UpdatePost(post);
         return result;      
      }

      public async Task<bool> DeletePost(int id)
      {
         var result = await _repository.DeletePost(id);
         return result;      
      }   
   }
}