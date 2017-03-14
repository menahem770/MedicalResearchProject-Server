using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MRP.DAL.Models
{
    public class MedicalInstitution
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}