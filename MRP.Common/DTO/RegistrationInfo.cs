using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP.Common.DTO
{
    public class RegistrationInfo
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactInfo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenceID { get; set; }
        public IEnumerable<MedicalInstitutionDTO> Institutions { get; set; } = new List<MedicalInstitutionDTO>();
    }
}
