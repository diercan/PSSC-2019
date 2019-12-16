using GameRentWeb.ExceptionClasses;
using GameRentWeb.Models;
using GameRentWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GameRentWeb.DDDModels
{
    public class UserDDD
    {
        List<User> _users;
        ReadOnlyCollection<User> Values { get { return _users.AsReadOnly(); } }
        public UserDDD(IDataBaseRepo<User> repo)
        {
            _users = repo.GetAllObjects().Result.ToList();
        }

        public bool IsFound(int id)
        {
            if(_users.Find(u => u.Id == id) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void UpdateUser(User user,IDataBaseRepo<User> repo)
        {
            if(IsFound(user.Id))
            {
                repo.Update(user);
                _users = repo.GetAllObjects().Result.ToList(); // update list after update
            }
            else
            {
                throw new ItemNotFound(user.UserName);
            }
        }
        
    }
}
