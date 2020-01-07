using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PSSC.Repository;
using PSSC.Models;
using System.Linq;

namespace PSSCTests
{

    class UserRepositoryShould
    {
        class MockedUserRepository : IUserRepository
        {
            public List<User> list = new List<User>();
            public void CreateUsersList( List<User> users)
            {
                list = users;
            }

            public User GetUser(string nume, string parola)
            {
                return list.FirstOrDefault(_ => _.Username == nume || _.Password == parola);
            }
        }

        class UserTestData
        {
            public List<User> list = new List<User>()
            {
                new User()
                {
                    Username = "1",
                    Password = "1",
                }

            };
        }

        [Test]
        public void GetUser()
        {
            //Arrage
            var repository = new MockedUserRepository();

            repository.CreateUsersList(new UserTestData().list);
            
            //Act
            var result = repository.GetUser("1", "1");

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result.Username == "1");
            Assert.That(result.Password == "1");
        }
    }
}
