using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MRP.DAL.Models
{
    public class MedicalInstitution
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public string Name { get; set; }
    }

}