using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SignalRChat.Models
{
    public class LoginModel
    {
        [BsonElement("Name")]
        public string UserName { get; set; }
        [BsonElement("Pass")]
        public string Pass { get; set; }
    }
}