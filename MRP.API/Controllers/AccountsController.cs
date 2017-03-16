using Microsoft.AspNet.Identity;
using MRP.API.Providers;
using MRP.BL;
using MRP.Common.DTO;
using System;
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
        [Route("Register"), HttpPost]
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

            return Created<UserDTO>("", null);
        }

        [Route("GetAllUsers"), HttpGet]
        public async Task<IHttpActionResult> GetAllUsersAsync()
        {
            try
            {
                IEnumerable<UserDTO> users = await _manager.GetAllUsersAsync();
                return users.GetEnumerator().Current != null ? Json(users) : (IHttpActionResult)BadRequest("no users found!"); ;
            }
            catch (Exception ex) { return InternalServerError(ex); }
        } 

        [Route("GetUser"), HttpGet]
        public async Task<IHttpActionResult> GetUserAsync([FromUri]string username)
        {
            try
            {
                UserDTO user = await _manager.GetUserAsync(username);
                return user != null ? Json(user) : (IHttpActionResult)BadRequest("no user found!");
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        [Route("GetUserByToken"), HttpPost]
        public async Task<IHttpActionResult> GetUserByTokenAsync([FromBody]string token)
        {
            string un = RequestContext.Principal.Identity.GetUserName();
            try
            {
                UserDTO user = await _manager.GetUserAsync(un);
                return user != null ? Json(user) : (IHttpActionResult)BadRequest("no user found!");
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        [AllowAnonymous]
        [Route("RecoverPassword"), HttpPost]
        public async Task<IHttpActionResult> RecoverPassword([FromBody]RecoveryInfo recInfo)
        {
            try
            {
                return await _manager.RecoverPasswordAsync(recInfo) ? Ok() : (IHttpActionResult)BadRequest("no user found!");
            }
            catch (Exception ex) { return InternalServerError(ex); }
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
