using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineBankingApp.Models
{
    public class Payee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNo { get; set; }

        public string AccountNo { get; set; }

        public string Description { get; set; }

    }
}