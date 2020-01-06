using Microsoft.EntityFrameworkCore;
using MVC_CookBook_PSSC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CookBook_PSSC.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext userDbContext)
        {
            _context = userDbContext;
        }
        public async Task<User> CreateAsync(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;

        }
        public async Task<List<User>> GetUsersToListAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(int? id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
        public async Task UpdateUserAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
        public DbSet<User> GetUsersAsDbSet()
        {
            return _context.Users;
        }
    }
}
