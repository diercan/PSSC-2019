using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSSC.Models;
using PSSC.Repository;

namespace PSSC.Services
{
    public interface ILoginService
    {
        public User ReturnValidUser(string nume, string parola);
    }
    public class LoginService : ILoginService
    {
        private readonly IUserRepository repository;

        public LoginService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public User ReturnValidUser(string nume, string parola)
        {
            return repository.GetUser(nume, parola);
        }
    }
}
