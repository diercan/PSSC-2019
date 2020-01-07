using HomeMadeMarketAPI.Models;
using HomeMadeMarketAPI.Models.UserLogin;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace HomeMadeMarketAPI.Services
{
    public class UserProfileService
    {
        private readonly IMongoCollection<UserProfile> _profile;

        public UserProfileService(IHomeMadeMarketDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _profile = database.GetCollection<UserProfile>(settings.ProfilesCollectionName);
        }

        public List<UserProfile> Get() =>
            _profile.Find(profile => true).ToList();

        public UserProfile Get(string id) =>
            _profile.Find<UserProfile>(profile => profile.Id == id).FirstOrDefault();

        public UserProfile Create(UserProfile profile)
        {
            _profile.InsertOne(profile);
            return profile;
        }

        public void Update(string id, UserProfile profileIn) =>
            _profile.ReplaceOne(profile => profile.Id == id, profileIn);

        public void Remove(UserProfile profileIn) =>
            _profile.DeleteOne(profile => profile.Id == profileIn.Id);

        public void Remove(string id) =>
            _profile.DeleteOne(profile => profile.Id == id);
    }
}