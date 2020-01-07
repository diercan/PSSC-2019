using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPlanner.Models;

namespace MyPlanner.Repository
{
    public interface IUserRepository
    {
        void AddItem(User item);
        List<User> GetAllItems();
    }
}
