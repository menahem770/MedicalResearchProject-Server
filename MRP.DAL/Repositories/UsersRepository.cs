using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using MRP.Common.DTO;
using MRP.Common.IRepositories;
using MRP.DAL.Models;
using MRP.DAL.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Configuration;
using System.Linq;


namespace MRP.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        MongoClient _client;
        IMongoDatabase _database;
        IMongoCollection<User> _users;
        UserStore<User> _store;
        UserManager<User> _userManager;
        //IMongoCollection<IdentityRole> _roles;
        //RoleStore<IdentityRole> _roleStore;


        public UsersRepository()
        {
            _client = new MongoClient(ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString);
            _database = _client.GetDatabase(ConfigurationManager.AppSettings.Get("MongoDbName"));
            _users = _database.GetCollection<User>("AspNetUsers");
            _store = new UserStore<User>(_users);
            _userManager = new UserManager<User>(_store);
            //_roles = _database.GetCollection<IdentityRole>("roles");
            //_roleStore = new RoleStore<IdentityRole>(_roles);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var collection = await _users.Find(u => u.Id != null).ToListAsync();
            return collection.ConvertToDTOExtension();
        }

        public async Task<UserDTO> GetUserAsync(string username)
        {
            var collection = await _users.Find(u => u.UserName == username).ToListAsync();
            return collection.ConvertToDTOExtension().ToList()[0];
        }

        
    }
}
