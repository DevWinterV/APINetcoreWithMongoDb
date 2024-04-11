using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace APINetcoreWithMongoDb.Entities
{
    public class Account : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [BsonElement("Id")]
        public int Id { get; set; }

        [BsonElement("UserId")]
        public int UserId { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Status")]
        public bool Status { get; set; }

        [BsonElement("AccountId")]
        public int AccountId { get; set; }
    }
}
