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
        public async Task<IHttpActionResult> GetPatients([FromBody]FindPatientModel model)
        {
            try
            {
                IEnumerable<PatientDTO> patients = await _manager.GetPatients(model);
                return patients.Count() > 0 ? Json(patients) : (IHttpActionResult)BadRequest("no patients found!");
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        [Route("AddPatient"),HttpPost]
        public async Task<IHttpActionResult> AddPatient([FromBody]PatientDTO patient)
        {
            try
            {
                return await _manager.AddPateint(patient) ? Created<PatientDiagnosisDTO>("", null) : (IHttpActionResult)BadRequest("changes not excepted!");
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        [Route("AddDiagnosis"),HttpPost]
        public async Task<IHttpActionResult> AddDiagnosis([FromBody]PatientDiagnosisDTO diagnosis)
        {
            try
            {
                return await _manager.AddDiagnosis(diagnosis) ? Created<PatientDiagnosisDTO>("",null) : (IHttpActionResult)BadRequest("changes not excepted!");
            }
            catch (Exception ex) { return InternalServerError(ex); }
            
        }

        [Route("EditPatient"),HttpPut]
        public async Task<IHttpActionResult> EditPatient([FromBody]PatientDTO patient)
        {
            try
            {
                var p = await _manager.EditPatient(patient);
                return p != null ? Ok(p) : (IHttpActionResult)BadRequest("changes not excepted!");
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        [Route("EditDiagnosis"),HttpPut]
        public async Task<IHttpActionResult> EditDiagnosis([FromBody]PatientDiagnosisDTO diagnosis)
        {
            try
            {
                var patient = await _manager.EditDiagnosis(diagnosis);
                return patient != null ? Ok(patient) : (IHttpActionResult)BadRequest("changes not excepted!");
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }
    }
}
