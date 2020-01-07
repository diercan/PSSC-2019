using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teambuilding.Models;
namespace Teambuilding.Repository
{
    public interface ICredentialsRepository
    {
        void addUser(Credentials user);
        List<Credentials> getCredentials();

        void deleteUser(Credentials user);
    }

    public class CredentialsRepository : ICredentialsRepository
    {

        private List<Credentials> users;
        public CredentialsRepository()
        {
            users = new List<Credentials>();
            users.Add(new Credentials("admin", "admin"));
            users.Add(new Credentials("marco", "admin"));
        }

        public void addUser(Credentials user)
        {
            users.Add(user);
        }

        public void deleteUser(Credentials user)
        {
            users.Add(user);
        }

        public List<Credentials> getCredentials()
        {
            return users;
        }

    }
}
