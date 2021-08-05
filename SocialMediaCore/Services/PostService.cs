using System;
using System.Linq;
using System.Threading.Tasks;
using SocialMediaCore.Entities;
using SocialMediaCore.Exceptions;
using SocialMediaCore.Interfaces;
using SocialMediaCore.QueryFilters;
using SocialMediaCore.CustomEntities;
using Microsoft.Extensions.Options;

namespace SocialMediaCore.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _repository;
        private readonly PaginationOptions _paginationOptions;

        public PostService(IUnitOfWork repository, IOptions<PaginationOptions> options)
        {
            _repository = repository;
            _paginationOptions = options.Value;
        }

        public PagedList<Post> GetPosts(PostQueryFilter postFilter)
        {
            // validar los campos del paginado
            postFilter.PageNumber = postFilter.PageNumber is null ? _paginationOptions.DefaultPageNumber : postFilter.PageNumber;
            postFilter.PageSize = postFilter.PageSize is null ? _paginationOptions.DefaulPageSize : postFilter.PageSize;
            // validar si alguno de los filtros viene nulo
            var posts = _repository.postRespository.GetAll();
            if(postFilter.IdUser != null)
                posts = posts.Where(p => p.IdUser == postFilter.IdUser).ToList();
            if(postFilter.Date != null)
                posts = posts.Where(p => p.Date.ToShortDateString() == postFilter.Date?.ToShortDateString()).ToList();
            if(!string.IsNullOrEmpty(postFilter.Description))
                posts = posts.Where(p => p.Description.ToLower().Contains(postFilter.Description.ToLower())).ToList();
            
            var postPaginated = PagedList<Post>.CreateList(posts,(int)postFilter.PageNumber,(int)postFilter.PageSize);

            return postPaginated;
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