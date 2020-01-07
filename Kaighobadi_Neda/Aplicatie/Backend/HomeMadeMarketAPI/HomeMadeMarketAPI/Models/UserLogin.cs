using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace HomeMadeMarketAPI.Models.UserLogin
{
    public class UserLogin
    {
        [Required]
        public string Username {get; set;}
        [Required]
        public string Password { get; set; }

    }
}
