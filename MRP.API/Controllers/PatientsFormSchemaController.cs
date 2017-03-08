using MRP.BL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MRP.API.Controllers
{
    [RoutePrefix("api/PatientsFormSchema"), Authorize]
    public class PatientsFormSchemaController : ApiController
    {
        private PatientsFormSchemaManager _manager;

        public PatientsFormSchemaController()
        {
            _manager = new PatientsFormSchemaManager();
        }

        [Route("SaveFirstSchema"), HttpPost]
        public async Task<IHttpActionResult> SaveFirstSchema(HttpRequestMessage request)
        {
            var jsonString = await request.Content.ReadAsStringAsync();
            if (await _manager.SaveFirstSchema(jsonString))
            {
                return Ok();
            }
            return InternalServerError();
        }

        [Route("GetFirstSchema"), HttpGet]
        public async Task<IHttpActionResult> GetFirstSchema()
        {
            string schema = await _manager.GetFirstSchema();
            return Ok(schema);
        }
    }
}
