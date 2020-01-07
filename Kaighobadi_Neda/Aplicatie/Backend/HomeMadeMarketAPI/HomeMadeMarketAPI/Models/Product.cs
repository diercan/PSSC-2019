using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace HomeMadeMarketAPI.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("id")]
        public string Id { get; set; }
        [BsonElement("pic")]
        [JsonProperty("pic")]
        public string Pic { get; set; }
        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name {get; set;}
        [BsonElement("category")]
        [JsonProperty("category")]
        public string Category { get; set; }
        [BsonElement("subcategory")]
        [JsonProperty("subcategory")]
        public string Subcategory { get; set; }
        [BsonElement("description")]
        [JsonProperty("description")]
        public string Description { get; set; }
        [BsonElement("price")]
        [JsonProperty("price")]
        public float Price { get; set; }
        [BsonElement("sellerId")]
        [JsonProperty("sellerId")]
        public string SellerId { get; set; }
        [BsonElement("sellerAdress")]
        [JsonProperty("sellerAdress")]
        public string SellerAdress { get; set; }

        //public void changeDescription(string description)
        //{
        //    this.Description = description;
        //}
        //public void changePrice(float price)
        //{
        //    this.Price = price;
        //}
        //public void changeType(string type)
        //{
        //    this.Type = type;
        //}
    }

}
