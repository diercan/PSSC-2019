using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook_MVC.Models;

namespace CookBook_MVC.Repositories
{
    interface IUserRepository
    {
        Task Create(User user);
        Task<List<User>> GetUsersAsync();
    }
}
