using Microsoft.EntityFrameworkCore;
using MVC_CookBook_PSSC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_CookBook_PSSC.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<List<User>> GetUsersToListAsync();
        Task<User> GetUserAsync(int? id);
        Task DeleteAsync(User user);
        bool UserExists(int id);
        Task UpdateUserAsync(User user);
        DbSet<User> GetUsersAsDbSet();
        

    }
}
