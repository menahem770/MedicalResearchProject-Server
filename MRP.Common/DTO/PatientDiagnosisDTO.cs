using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP.Common.DTO
{
    public class PatientDiagnosisDTO
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public MedicalInstitutionDTO MedicalInstitution { get; set; }
        public bool InOutPatient { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime InclusionDate { get; set; }
        public string General { get; set; }
        public Dictionary<string, dynamic> Symptoms { get; set; } = new Dictionary<string, dynamic>();
    }
}
