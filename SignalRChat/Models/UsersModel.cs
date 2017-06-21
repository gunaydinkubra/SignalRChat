using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SignalRChat.Models
{
    public class UsersModel
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [BsonElement("Name")] 
        public string Name { get; set; }

        [BsonElement("Surname")]
        [Required(ErrorMessage = "Please enter your surname")]
        public string Surname { get; set; }

        [BsonElement("UserName")]
        [Required(ErrorMessage = "Please enter your username")]
        public string UserName { get; set; }

        [BsonElement("Pass")]
        [Required(ErrorMessage = "Please enter your password")]
        public string Pass { get; set; }

    }
}