using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace HomeMadeMarketAPI.Models
{
    public class UserProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        [JsonProperty("name")]
        public string FullName { get; set; }
        [BsonElement("email")]
        [JsonProperty("email")]
        public string Email { get; set; }
        [BsonElement("description")]
        [JsonProperty("description")]
        public string Description { get; set; }
        [BsonElement("isSeller")]
        [JsonProperty("isSeller")]
        public string IsSeller { get; set; }
    }
}
