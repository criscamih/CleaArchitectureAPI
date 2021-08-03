using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMediaCore.Entities;
using SocialMediaCore.Interfaces;
using SocialMediaInfrastructure.Data;

namespace SocialMediaInfrastructure.Repositories
{   
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly BlogContext _context;
        protected readonly DbSet<T> _entity;
        public BaseRepository(BlogContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public async Task<T> GetById(int id)
        {
            return await _entity.FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return  _entity.AsEnumerable();   
        }
        public async Task Add(T entity)
        {
           await _entity.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entityToDelete = await GetById(id);
            _entity.Remove(entityToDelete);
        }


        public void Update(T entity)
        {
            _entity.Update(entity);
        }
    }
}