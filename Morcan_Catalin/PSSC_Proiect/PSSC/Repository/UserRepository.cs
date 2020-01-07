using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSSC.Models;

namespace PSSC.Repository
{
    public interface IUserRepository
    {
        public User GetUser(string nume, string parola);

    }

    public class UserRepository : IUserRepository
    {
        private readonly List<User> listaUseri;

        public UserRepository()
        {
            listaUseri = new List<User>();
            listaUseri.Add(new User
            {
                Username = "admin",
                Password = "admin"
            }) ;

            listaUseri.Add(new User
            {
                Username = "a",
                Password = "a"
            });
        }

        public User GetUser(string nume, string parola)
        {
            return listaUseri.FirstOrDefault(_ => _.Username == nume || _.Password==parola);
        }

    }
}
