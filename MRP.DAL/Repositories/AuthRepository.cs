using System.Linq;
using System.Threading.Tasks;
using MRP.Common.DTO;
using MRP.DAL.Models;
using MRP.DAL.Services;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using AspNet.Identity.MongoDB;
using MRP.Common.IRepositories;
using System.Configuration;

namespace MRP.DAL.Repositories
{
    public class AuthRepository : IAuthRpository
    {
        MongoClient _client;
        IMongoDatabase _database;
        IMongoCollection<User> _users;
        //IMongoCollection<IdentityRole> _roles;
        UserStore<User> _store;
        //RoleStore<IdentityRole> _roleStore;
        UserManager<User> _userManager;


        public AuthRepository()
        {
            _client = new MongoClient(ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString);
            _database = _client.GetDatabase("MRPDB"/*ConfigurationManager.AppSettings.Get("MRPDB")*/);
            _users = _database.GetCollection<User>("AspNetUsers");
            _store = new UserStore<User>(_users);
            _userManager = new UserManager<User>(_store);
            //_roles = _database.GetCollection<IdentityRole>("roles");
            //_roleStore = new RoleStore<IdentityRole>(_roles);
        }

        public async Task<UserDTO> Login(string username, string password)
        {
            User user = await _userManager.FindAsync(username, password);
            return user.ConvertToDTO();
        }

        public async Task<IdentityResult> Register(RegistrationInfo regInfo)
        {
            var user = new User
            {
                UserName = regInfo.Username,
                FullName = regInfo.FullName,
                Email = regInfo.EmailAddress,
                DateOfBirth = regInfo.DateOfBirth,
                ContactInfo = regInfo.ContactInfo,
                LicenceID = regInfo.LicenceID,
                Institutions = regInfo.Institutions.ConvertToModelExtension().ToList()
            };
            return await _userManager.CreateAsync(user, regInfo.Password);
        }

        private async Task<UserDTO> FindUser(string username, string password)
        {
            User user = await _userManager.FindAsync(username, password);
            return user.ConvertToDTO();
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
        //public PasswordRecoveryResponse RecoverPassword(RecoveryInfo recInfo)
        //{
        //    User user = _users.FirstOrDefault(u => u.EmailAddress == recInfo.EmailAddress && u.DateOfBirth == recInfo.DateOfBirth);
        //    if(user != null)
        //    {
        //        user.Password = RandomPasswordGenerator.GeneratePassword(8);
        //        return new PasswordRecoveryResponse { Success = true, TempPassword = user.Password };
        //    }
        //    else
        //    {
        //        return new PasswordRecoveryResponse { Message = "User not found!" };
        //    }
        //}
        //public IEnumerable<UserDTO> GetUsers()
        //{
        //    return _users.ConvertToDTOExtension().ToList();
        //}

        //public UserDTO GetUser(int id)
        //{
        //    return _users.Where(u => u.ID == id).ConvertToDTOExtension().FirstOrDefault();
        //}

        //public bool DeleteUser(int id)
        //{
        //    return _users.Remove(_users.Find(u => u.ID == id));
        //}

        //public bool EditUser(int id, EditUserInfo uInfo)
        //{
        //    User user = _users.Find(u => u.ID == id);
        //    if (user != null)
        //    {
        //        user.FullName = uInfo.FullName;
        //        user.EmailAddress = uInfo.Emailddress;
        //        user.Username = uInfo.Username;
        //        user.ContactInfo = uInfo.ContactInfo;
        //        user.AuthLevel = uInfo.AuthLevel;
        //        user.LicenceID = uInfo.LicenceID;
        //        user.Institutions = uInfo.Institutions.ConvertToModelExtension().ToList();
        //        return true;
        //    }
        //    return false;
        //}
    }
}
