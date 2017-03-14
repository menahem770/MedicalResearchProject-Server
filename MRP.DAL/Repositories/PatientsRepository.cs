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
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

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
            _database = _client.GetDatabase(ConfigurationManager.AppSettings.Get("MongoDbName"));
            _patients = _database.GetCollection<Patient>("Patients");
        }

        public async Task<bool> AddDiagnosis(PatientDiagnosisDTO diagnosis)
        {
            var update = Builders<Patient>.Update.CurrentDate("LastModified").AddToSet(p => p.Diagnosis, diagnosis.ConvertToModel());
            var res = await _patients.UpdateOneAsync(p => p.PatientId == diagnosis.PatientId, update);
            return res.IsAcknowledged;
        }

        public async Task<bool> AddPatient(PatientDTO patient)
        {
            await _patients.InsertOneAsync(patient.ConvertToModel());
            return true;
        }

        public async Task<PatientDTO> EditPatientInfo(PatientDTO patient)
        {
            Patient clientPatient = patient.ConvertToModel();
            Patient dbPatient = _patients.Find(p => p.PatientId == clientPatient.PatientId).First();
            var updates = new List<UpdateDefinition<Patient>> { Builders<Patient>.Update.CurrentDate("LastModified") };
            string[] unchanged = { "Id", "PatientId", "LastModified", "Diagnosis" };
            IEnumerable<PropertyInfo> properties = typeof(Patient).GetProperties().Where(p => !unchanged.Contains(p.Name));
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.CanRead)
                {
                    object firstValue = propertyInfo.GetValue(clientPatient);
                    object secondValue = propertyInfo.GetValue(dbPatient);
                    if (!Equals(firstValue, secondValue))
                    {
                        updates.Add(Builders<Patient>.Update.Set(propertyInfo.Name, firstValue));
                    }
                }
            }
            var resPatient = await _patients.FindOneAndUpdateAsync(p => p.PatientId == clientPatient.PatientId, Builders<Patient>.Update.Combine(updates));
            return resPatient.ConvertToDTO();
        }

        public async Task<PatientDTO> EditDiagnosis(PatientDiagnosisDTO diagnosis)
        {
            var patient = await _patients.FindOneAndUpdateAsync(p =>
                    p.PatientId == diagnosis.PatientId && p.Diagnosis.Any(d => d.Id == diagnosis.Id),
                    Builders<Patient>.Update.Set(p => p.Diagnosis.ElementAt(-1), diagnosis.ConvertToModel()));
            return patient.ConvertToDTO();
        }

        public async Task<IEnumerable<PatientDTO>> GetPatients(FindPatientModel model)
        {
            List<Patient> collection;
            if (!String.IsNullOrWhiteSpace(model.PatientId))
                collection = await _patients.Find(p => p.PatientId == model.PatientId).ToListAsync();
            else
                collection = await _patients.Find(p => p.Name == model.Name).ToListAsync();
            return collection.ConvertToDTOExtension().ToList();
        }
    }
}
