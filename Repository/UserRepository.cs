using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class UserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<User> ByCredentials(string login, string password)
        {
            return await _databaseContext.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
        }

        public async Task<User> ByLogin(string login)
        {
            return await _databaseContext.Users.FirstOrDefaultAsync(x => x.Login == login);
        }
        
        public async Task<User> ById(Guid id)
        {
            return await _databaseContext.Users.FindAsync(id);
        }
        
        public async Task<User> CreateUser(User user)
        {
            var entity = await _databaseContext.Users.AddAsync(user);
            await _databaseContext.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task DeleteUser(Guid userId)
        {
            var user = await _databaseContext.Users.FindAsync(userId);
            if (user is not null)
            {
                _databaseContext.Users.Remove(user);
                await _databaseContext.SaveChangesAsync();
            }
        }

        public async Task<User> UpdatePassword(Guid userId, string password)
        {
            var user = await _databaseContext.Users.FindAsync(userId);
            if (user is not null)
            {
                user.Password = password;
                _databaseContext.Users.Update(user);
                await _databaseContext.SaveChangesAsync();
            }

            return user;
        }
    }
}