using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace HomeMadeMarketAPI.Models
{
    public class User
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        [JsonProperty("name")]
        public string FullName { get; set; }
        [BsonElement("phone")]
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [BsonElement("email")]
        [JsonProperty("email")]
        public string Email { get; set; }
        [BsonElement("username")]
        [JsonProperty("username")]
        public string Username { get; set; }
        [BsonElement("password")]
        [JsonProperty("password")]
        public string Password { get; set; }
        [BsonElement("adress")]
        [JsonProperty("adress")]
        public Adress Adress { get; set; }
        [BsonElement("isSeller")]
        [JsonProperty("isSeller")]
        public bool IsSeller { get; set; }
    }
}
