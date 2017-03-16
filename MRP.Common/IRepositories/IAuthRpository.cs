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
        Task<IdentityResult> Register(RegistrationInfo regInfo);
        Task<UserDTO> Login(string username, string password);
        Task<bool> RecoverPasswordAsync(RecoveryInfo recInfo);
    }
}
