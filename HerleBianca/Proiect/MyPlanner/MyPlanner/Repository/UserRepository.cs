using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPlanner.Models;

namespace MyPlanner.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly List<User> _items = new List<User>();

        public void AddItem(User item)
        {
            _items.Add(item);
        }

        public List<User> GetAllItems()
        {
            return _items;
        }
       
    }
}
