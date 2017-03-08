using MRP.BL;
using MRP.Common.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace MRP.API.Controllers
{
    [RoutePrefix("api/Patients"),Authorize]
    public class PatientsController : ApiController
    {
        private PatientsManager _manager;

        public PatientsController()
        {
            _manager = new PatientsManager();
        }

        [Route("GetPatients"),HttpPost]
        public async Task<JsonResult<IEnumerable<PatientDTO>>> GetPatients([FromBody]FindPatientModel model)
        {
            IEnumerable<PatientDTO> patients = await _manager.GetPatients(model);
            return Json(patients);
        }

        [Route("AddPatient"),HttpPost]
        public async Task<IHttpActionResult> AddPatient([FromBody]PatientDTO patient)
        {
            try
            {
                await _manager.AddPateint(patient);
                return Ok();
            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message,null);
            }
        }

        [Route("AddDiagnosis"),HttpPut]
        public async Task<IHttpActionResult> AddDiagnosis()
        {
            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            return await _manager.AddDiagnosis(jsonContent) ? Created<PatientDiagnosisDTO>("", null) : (IHttpActionResult)InternalServerError();
        }

        [Route("EditPatient"),HttpPut]
        public async Task<IHttpActionResult> EditPatient([FromBody]PatientDTO patient)
        {
            return await _manager.EditPatient(patient) ? Created<PatientDTO>("", null) : (IHttpActionResult)InternalServerError();
        }
    }
}
