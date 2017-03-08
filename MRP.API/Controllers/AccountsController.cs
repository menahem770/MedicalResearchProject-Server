using Microsoft.AspNet.Identity;
using MRP.API.Models;
using MRP.API.Providers;
using MRP.BL;
using MRP.Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace MRP.API.Controllers
{
    [RoutePrefix("api/Accounts"), Authorize]
    public class AccountsController : ApiController
    {
        private UserAccountsManager _manager;

        public AccountsController()
        {
            _manager = new UserAccountsManager();
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register([FromBody]RegistrationInfo info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _manager.CreateAsync(info);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Created<UserDTO>("",null);
        }

        [Route("GetAllUsers")]
        public async Task<JsonResult<IEnumerable<UserDTO>>> GetAllUsersAsync()
        {
            IEnumerable<UserDTO> users = await _manager.GetAllUsersAsync();
            return Json(users);
        }

        [Route("GetUser")]
        public async Task<JsonResult<UserDTO>> GetUserAsync([FromUri]string username)
        {
            UserDTO user = await _manager.GetUserAsync(username);
            return Json(user);
        }

        [Route("GetUserByToken")]
        public async Task<JsonResult<UserDTO>> GetUserByTokenAsync([FromBody]string token)
        {
            string un = RequestContext.Principal.Identity.GetUserName();
            UserDTO user = await _manager.GetUserAsync(un);
            return Json(user);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
