using Microsoft.AspNet.Identity;
using MRP.Common.DTO;
using MRP.Common.IRepositories;
using MRP.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRP.BL
{
    public class UserAccountsManager
    {
        IAuthRpository authRep;
        IUsersRepository userRep;
        public UserAccountsManager(IAuthRpository ar,IUsersRepository ur)
        {
            authRep = ar;
            userRep = ur;
        }
        public UserAccountsManager()
        {
            authRep = new AuthRepository();
            userRep = new UsersRepository();
        }

        
        public Task<UserDTO> Login(string username, string password)
        {
            return authRep.Login(username,password);
        }

        public Task<IdentityResult> CreateAsync(RegistrationInfo regInfo)
        {
            return authRep.Register(regInfo);
        }

        public Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            return userRep.GetAllUsersAsync();
        }

        public Task<UserDTO> GetUserAsync(string username)
        {
            return userRep.GetUserAsync(username);
        }


        //public IEnumerable<UserDTO> GetUsers()
        //{
        //    return rep.GetUsers();
        //}

        //public UserDTO GetUsers(int id)
        //{
        //    return rep.GetUser(id);
        //}

        //public bool EditUser(int id, EditUserInfo uInfo)
        //{
        //    return rep.EditUser(id, uInfo);
        //}
        //public Task<UserDTO> FindUser(string username, string password)
        //{
        //    return rep.FindUser(username, password);
        //}

        //public bool DeleteUser(int id)
        //{
        //    return rep.DeleteUser(id);
        //}

        //public PasswordRecoveryResponse RecoverPassword(RecoveryInfo recInfo)
        //{
        //    return rep.RecoverPassword(recInfo);
        //}
    }
}
