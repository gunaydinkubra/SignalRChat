using MongoDB.Driver;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Db
{
    public class DbSet : IDisposable
    {
        MongoClient _client;
        IMongoDatabase _db;

        IMongoCollection<UsersModel> _collectionUser;
        public DbSet()
        {
            _client = new MongoClient("mongodb://kubra:littleprince@ds133922.mlab.com:33922/dbchat");
            _db = _client.GetDatabase("dbchat");
        }
        public IMongoCollection<UsersModel> Users
        {
            get
            {
                _collectionUser = _db.GetCollection<UsersModel>("Users");
                return _collectionUser;
            }
        }
        public  void Add(UsersModel user)
        {  
            Users.InsertOne(user);
        }
        public UsersModel GetUser(string userName)
        {
            var user = Users.Find(a => a.UserName == userName).FirstOrDefault();
            return user;
        }


        #region Disposible
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    #endregion
    }
}