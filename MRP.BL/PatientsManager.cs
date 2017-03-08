using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRP.Common.DTO;
using MRP.Common.IRepositories;
using MRP.DAL.Repositories;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MRP.BL
{
    public class PatientsManager
    {
        private IPatientsRepository _pRep;

        public PatientsManager()
        {
            _pRep = new PatientsRepository();
        }

        public Task<IEnumerable<PatientDTO>> GetPatients(FindPatientModel model)
        {
            return _pRep.GetPatients(model);
        }

        public Task<bool> AddPateint(PatientDTO patient)
        {
            return _pRep.AddPatient(patient);
        }

        public async Task<bool> AddDiagnosis(string requestContent)
        {
            PatientDiagnosisDTO diagnosis = await GetSymptomsFromRequest(requestContent);
            return await _pRep.AddDiagnosis(diagnosis);
        }

        public Task<bool> EditPatient(PatientDTO patient)
        {
            return _pRep.EditPatientInfo(patient);
        }

        private async Task<PatientDiagnosisDTO> GetSymptomsFromRequest(string requestContent)
        {
            PatientDiagnosisDTO diagnosis = new PatientDiagnosisDTO();
            await Task.Factory.StartNew(() =>
            {
                dynamic json = JValue.Parse(requestContent);
                dynamic symptoms = json.Symptoms;
                json.Symptoms = null;
                string str = JsonConvert.SerializeObject(json);
                diagnosis = JsonConvert.DeserializeObject<PatientDiagnosisDTO>(str);
                diagnosis.Symptoms = new Dictionary<string, SymptomInfo>();
                foreach (var s in symptoms)
                {
                    str = JsonConvert.SerializeObject(s.Symptom);
                    SymptomInfo info = JsonConvert.DeserializeObject<SymptomInfo>(str);
                    diagnosis.Symptoms.Add(info.SymptomName, info);
                }
            });
            return diagnosis;
        }

    }
}
