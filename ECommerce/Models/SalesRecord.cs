using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Ecommerce.Models
{
    public class SalesRecord
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
