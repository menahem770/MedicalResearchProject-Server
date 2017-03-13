using AspNet.Identity.MongoDB;
using MongoDB.Bson.Serialization.Attributes;
using MRP.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP.DAL.Models
{
    public class User : IdentityUser
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string ContactInfo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenceID { get; set; }
        public IEnumerable<MedicalInstitution> Institutions { get; set; } = new List<MedicalInstitution>();
    }
}
