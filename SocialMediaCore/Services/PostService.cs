using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialMediaCore.Entities;
using SocialMediaCore.Exceptions;
using SocialMediaCore.Interfaces;

namespace SocialMediaCore.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _repository;

        public PostService(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public IEnumerable<Post> GetPosts()
        {
            return _repository.postRespository.GetAll();
        }

        public async Task<Post> GetPost(int idPost)
        {
            var post = await _repository.postRespository.GetById(idPost);
            return post;
        }

        public async Task InsertPost(Post post)
        {
            // validar si el usuario existe en la base de datos.
            var user = await _repository.userRespository.GetById(post.IdUser);
            if (user is null)
                throw new BussinesException("User Doesn't exist");
            // validar que no se pueda usar la palabra sexo.
            if (post.Description.Contains("sexo"))
                throw new BussinesException("Content not allowed");
            // si el usuario tiene menos de 10 post y el últimos post fue menos de 7 días no se puede publicar
            var posts = await _repository.postRespository.GetPostsByIdUser(post.IdUser);
            if (posts.Count() < 10)
            {
               var lastPost = posts.OrderByDescending(d => d.Date).FirstOrDefault();
               if ((DateTime.Now - lastPost.Date).TotalDays < 7)
               {
                  throw new BussinesException("User is not allowed to publish a post!!");
               }
            }
            await _repository.postRespository.Add(post);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            _repository.postRespository.Update(post);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _repository.postRespository.Delete(id);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}