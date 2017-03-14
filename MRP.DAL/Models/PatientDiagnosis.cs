using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using MRP.Common.DTO;
using System;
using System.Collections.Generic;

namespace MRP.DAL.Models
{
    public class PatientDiagnosis
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public MedicalInstitution MedicalInstitution { get; set; }
        public bool InOutPatient { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public DateTime InclusionDate { get; set; }
        public string General { get; set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
        public Dictionary<string,dynamic> Symptoms { get; set; } = new Dictionary<string, dynamic>();
    }
}