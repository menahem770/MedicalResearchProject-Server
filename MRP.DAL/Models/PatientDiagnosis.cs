using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MRP.Common.DTO;
using System;
using System.Collections.Generic;

namespace MRP.DAL.Models
{
    public class PatientDiagnosis
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public MedicalInstitution MedicalInstitution { get; set; }
        public bool InOutPatient { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime InclusionDate { get; set; }
        public string General { get; set; }
        public Dictionary<string,SymptomInfo> Symptoms { get; set; } = new Dictionary<string, SymptomInfo>();
    }
}