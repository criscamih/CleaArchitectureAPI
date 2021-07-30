using System;
using System.Linq;
using SocialMediaCore.Entities;
using System.Collections.Generic;
using SocialMediaCore.Interfaces;
using System.Threading.Tasks;
using SocialMediaInfrastructure.Data;


namespace SocialMediaInfrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _context;
        public UserRepository(BlogContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                var users = _context.Users.ToList();
                return await Task.FromResult(users);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<User> GetUser(int idUser)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(p => p.IdUser == idUser);
                return await Task.FromResult(user);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}