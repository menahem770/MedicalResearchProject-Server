using MRP.Common.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRP.Common.DTO;
using MRP.DAL.Models;
using MongoDB.Driver;
using AspNet.Identity.MongoDB;
using Microsoft.AspNet.Identity;
using System.Configuration;
using MRP.DAL.Services;
using System.Reflection;

namespace MRP.DAL.Repositories
{
    public class PatientsRepository : IPatientsRepository
    {
        MongoClient _client;
        IMongoDatabase _database;
        IMongoCollection<Patient> _patients;

        public PatientsRepository()
        {
            _client = new MongoClient(ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString);
            _database = _client.GetDatabase("MRPDB"/*ConfigurationManager.AppSettings.Get("MRPDB")*/);
            _patients = _database.GetCollection<Patient>("Patients");
        }

        public async Task<bool> AddDiagnosis(PatientDiagnosisDTO diagnosis)
        {
            var update = Builders<Patient>.Update.CurrentDate("LastModified").AddToSet(p => p.Diagnosis, diagnosis.ConvertToModel());
            try
            {
                await _patients.UpdateOneAsync(p => p.PatientId == diagnosis.PatientId, update);
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> AddPatient(PatientDTO patient)
        {
            try
            {
                await _patients.InsertOneAsync(patient.ConvertToModel());
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> EditPatientInfo(PatientDTO patient)
        {
            Patient clientPatient = patient.ConvertToModel();
            List<Patient> dbPatient = await _patients.Find(p => p.PatientId == clientPatient.PatientId).ToListAsync();
            var updates = new List<UpdateDefinition<Patient>>();
            updates.Add(Builders<Patient>.Update.CurrentDate("LastModified"));
            string[] unchanged = { "Id", "PatientId", "LastModified", "Diagnosis" };
            IEnumerable<PropertyInfo> properties = typeof(Patient).GetProperties().Where(p => !unchanged.Contains(p.Name));
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.CanRead)
                {
                    object firstValue = propertyInfo.GetValue(clientPatient);
                    object secondValue = propertyInfo.GetValue(dbPatient[0]);
                    if (!Equals(firstValue, secondValue))
                    {
                        updates.Add(Builders<Patient>.Update.Set(propertyInfo.Name, firstValue));
                    }
                }
            }
            try
            {
                var update = Builders<Patient>.Update.Combine(updates);
                await _patients.UpdateOneAsync(p => p.PatientId == clientPatient.PatientId, update);
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<IEnumerable<PatientDTO>> GetPatients(FindPatientModel model)
        {
            List<Patient> collection;
            if (!String.IsNullOrWhiteSpace(model.PatientId))
            {
                try
                {
                    collection = await _patients.Find(p => p.PatientId == model.PatientId).ToListAsync();
                    return collection.ConvertToDTOExtension().ToList();
                }
                catch (Exception ex) { throw ex; }
            }
            collection = await _patients.Find(p => p.Name == model.Name).ToListAsync();
            return collection.ConvertToDTOExtension().ToList();
        }
    }
}
