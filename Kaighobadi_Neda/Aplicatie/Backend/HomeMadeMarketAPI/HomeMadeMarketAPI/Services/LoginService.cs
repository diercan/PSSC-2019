using HomeMadeMarketAPI.Models;
using HomeMadeMarketAPI.Models.UserLogin;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMadeMarketAPI.Helpers;

namespace HomeMadeMarketAPI.Services
{
    public interface ILoginService {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }

    public class LoginService :ILoginService
    {
        private readonly IMongoCollection<User> _loginInfo;

        public LoginService(IHomeMadeMarketDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _loginInfo = database.GetCollection<User>(settings.UserCollectionName);
        }

       public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _loginInfo.Find<User>(user => user.Username == username && user.Password == password).FirstOrDefault());
            if(user==null)
            {
                return null;
            }
            return user.WithoutPassword();

        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => _loginInfo.Find(user => true).ToList().WithoutPasswords());
        }
        public User Get(string id) =>
           _loginInfo.Find<User>(user => user.Id == id).FirstOrDefault();
        public User Create(User user)
        {
            _loginInfo.InsertOne(user);
            return user;
        }

    }
}