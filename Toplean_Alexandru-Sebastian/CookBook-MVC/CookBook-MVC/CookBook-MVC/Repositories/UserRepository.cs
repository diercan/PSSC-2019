using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBook_MVC.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext userDbContext)
        {
            _context = userDbContext;
        }
        public async Task Create(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
