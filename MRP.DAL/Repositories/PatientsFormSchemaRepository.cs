using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MRP.Common.IRepositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP.DAL.Repositories
{
    public class PatientsFormSchemaRepository : IPatientsFormSchemaRepository
    {
        MongoClient _client;
        IMongoDatabase _database;
        IMongoCollection<BsonDocument> _patientsSchema;

        public PatientsFormSchemaRepository()
        {
            _client = new MongoClient(ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString);
            _database = _client.GetDatabase(ConfigurationManager.AppSettings.Get("MongoDbName"));
            _patientsSchema = _database.GetCollection<BsonDocument>("PatientsFormSchema");
        }
        public async Task<bool> SaveFirstSchema(string schema)
        {
            List<BsonDocument> arr = BsonSerializer.Deserialize<BsonArray>(schema).Select(d => d.AsBsonDocument).ToList<BsonDocument>();
            await _patientsSchema.InsertManyAsync(arr);
            return true;
        }

        public async Task<string> GetFirstSchema()
        {
            List<BsonDocument> result = await _patientsSchema.Find(new BsonDocument())
                .Project(Builders<BsonDocument>.Projection.Exclude("_id")).ToListAsync();
            return result.ToJson();
        }
    }
}
