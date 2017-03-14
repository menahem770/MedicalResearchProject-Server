using MRP.Common.DTO;
using MRP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP.DAL.Services
{
    public static class LinqExtention
    {
        #region user
        public static IEnumerable<UserDTO> ConvertToDTOExtension(this IEnumerable<User> users)
        {
            foreach (User u in users)
            {
                yield return u.ConvertToDTO();
            }
        }
        #endregion
        #region medical institution
        public static IEnumerable<MedicalInstitutionDTO> ConvertToDTOExtension(this IEnumerable<MedicalInstitution> ins)
        {
            if (ins != null)
            {
                foreach (MedicalInstitution m in ins)
                {
                    yield return m.ConvertToDTO();
                }
            }
        }
        public static IEnumerable<MedicalInstitution> ConvertToModelExtension(this IEnumerable<MedicalInstitutionDTO> ins)
        {
            if (ins != null)
            {
                foreach (MedicalInstitutionDTO m in ins)
                {
                    yield return m.ConvertToModel();
                }
            }
        }
        #endregion
        #region patient
        public static IEnumerable<PatientDTO> ConvertToDTOExtension(this IEnumerable<Patient> patients)
        {
            if (patients != null)
            {
                foreach (Patient p in patients)
                {
                    yield return p.ConvertToDTO();
                }
            }
        }
        public static IEnumerable<Patient> ConvertToModelExtension(this IEnumerable<PatientDTO> patients)
        {
            if (patients != null)
            {
                foreach (PatientDTO p in patients)
                {
                    yield return p.ConvertToModel();
                }
            }
        }
        #endregion
        #region patient diagnosis
        public static IEnumerable<PatientDiagnosisDTO> ConvertToDTOExtension(this IEnumerable<PatientDiagnosis> dia)
        {
            if (dia != null)
            {
                foreach (PatientDiagnosis d in dia)
                {
                    yield return d.ConvertToDTO();
                }
            }
        }
        public static IEnumerable<PatientDiagnosis> ConvertToModelExtension(this IEnumerable<PatientDiagnosisDTO> dia)
        {
            if (dia != null)
            {
                foreach (PatientDiagnosisDTO d in dia)
                {
                    yield return d.ConvertToModel();
                }
            }
        }

        #endregion
    }

    public static class UserDTOExtention
    {
        public static UserDTO ConvertToDTO(this User u)
        {
            return u != null ? new UserDTO
            {
                Id = u.Id,
                UserId = u.UserId,
                UserName = u.UserName,
                EmailAddress = u.Email,
                FullName = u.FullName,
                DateOfBirth = u.DateOfBirth,
                ContactInfo = u.ContactInfo,
                Roles = u.Roles,
                LicenceID = u.LicenceID,
                Institutions = u.Institutions.ConvertToDTOExtension().ToList()
            } : null;
        }
    }

    public static class PatientDTOExtention
    {
        public static PatientDTO ConvertToDTO(this Patient p)
        {
            return p != null ? new PatientDTO
            {
                Id = p.Id,
                PatientId = p.PatientId,
                Name = p.Name,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                Race = p.Race,
                InclusionDate = p.InclusionDate,
                General = p.General,
                LastModified = p.LastModified,
                Diagnosis = p.Diagnosis.ConvertToDTOExtension().ToList()
            } : null;
        }

        public static PatientDiagnosisDTO ConvertToDTO(this PatientDiagnosis d)
        {
            return d != null ? new PatientDiagnosisDTO
            {
                Id = d.Id,
                PatientId = d.PatientId,
                DoctorId = d.DoctorId,
                DoctorName = d.DoctorName,
                MedicalInstitution = d.MedicalInstitution.ConvertToDTO(),
                InOutPatient = d.InOutPatient,
                DiagnosisDate = d.DiagnosisDate,
                DischargeDate = d.DischargeDate,
                InclusionDate = d.InclusionDate,
                General = d.General,
                Symptoms = d.Symptoms
            } : null;
        }

        public static MedicalInstitutionDTO ConvertToDTO(this MedicalInstitution ins)
        {
            return ins != null ? new MedicalInstitutionDTO { Id = ins.Id, Name = ins.Name } : null;
        }

        public static Patient ConvertToModel(this PatientDTO p)
        {
            return p != null ? new Patient
            {
                Id = p.Id,
                PatientId = p.PatientId,
                Name = p.Name,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                Race = p.Race,
                InclusionDate = p.InclusionDate,
                General = p.General,
                LastModified = p.LastModified,
                Diagnosis = p.Diagnosis.ConvertToModelExtension().ToList()
            } : null;
        }

        public static PatientDiagnosis ConvertToModel(this PatientDiagnosisDTO d)
        {
            return d != null ? new PatientDiagnosis
            {
                Id = d.Id,
                PatientId = d.PatientId,
                DoctorId = d.DoctorId,
                DoctorName = d.DoctorName,
                MedicalInstitution = d.MedicalInstitution.ConvertToModel(),
                InOutPatient = d.InOutPatient,
                DiagnosisDate = d.DiagnosisDate,
                DischargeDate = d.DischargeDate,
                InclusionDate = d.InclusionDate,
                General = d.General,
                Symptoms = d.Symptoms
            } : null;
        }

        public static MedicalInstitution ConvertToModel(this MedicalInstitutionDTO ins)
        {
            return ins != null ? new MedicalInstitution { Id = ins.Id, Name = ins.Name } : null;
        }
    }

}
