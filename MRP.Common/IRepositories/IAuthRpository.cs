using Microsoft.AspNet.Identity;
using MRP.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP.Common.IRepositories
{
    public interface IAuthRpository
    {
        //IEnumerable<UserDTO> GetUsers();
        //UserDTO GetUser(int id);
        //UserDTO Login(LoginInfo logInfo);
        Task<IdentityResult> Register(RegistrationInfo regInfo);
        //Task<UserDTO> FindUser(string username, string password);
        //Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> Login(string username, string password);
        //PasswordRecoveryResponse RecoverPassword(RecoveryInfo recInfo);
        //bool DeleteUser(int id);
        //bool EditUser(int id, EditUserInfo uInfo);
        //PatientDTO GetPatient(PatientQuery pq);
    }
}
