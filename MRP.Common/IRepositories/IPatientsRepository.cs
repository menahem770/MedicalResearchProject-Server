﻿using MRP.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP.Common.IRepositories
{
    public interface IPatientsRepository
    {
        Task<IEnumerable<PatientDTO>> GetPatients(FindPatientModel model);
        Task<bool> AddPatient(PatientDTO patient);
        Task<bool> AddDiagnosis(PatientDiagnosisDTO diagnosis);
        Task<bool> EditPatientInfo(PatientDTO patient);
        Task<bool> EditDiagnosis(PatientDiagnosisDTO diagnosis);
    }
}
