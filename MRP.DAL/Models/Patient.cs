using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MRP.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP.DAL.Models
{
    public class Patient
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string PatientId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Race Race { get; set; }
        public DateTime InclusionDate { get; set; }
        public string General { get; set; }
        public DateTime LastModified { get; set; }
        public IEnumerable<PatientDiagnosis> Diagnosis { get; set; } = new List<PatientDiagnosis>();
    }
}
