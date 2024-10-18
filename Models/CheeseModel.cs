using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace PZCheeseriaRestApi.Models
{
    public class CheeseModel
    {
        const string CHEESE_NAME = "cheese_name";
        const string CHEESE_PRICE_PER_KILO = "cheese_price_per_kilo";
        const string CHEESE_PRICE_PER_POUND = "cheese_price_per_pound";
        const string CHEESE_COLOR = "cheese_color";
        const string CHEESE_IMAGE_URL = "cheese_image_url";
        const string CHEESE_DESCRIPTION = "cheese_description";
        const string CHEESE_ORIGIN = "cheese_origin";
        const string CHEESE_STOCK = "cheese_stock";
        const string CREATED_AT = "created_at";
        const string UPDATED_AT = "updated_at";

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string id { get; set; }

        [BsonElement(CHEESE_NAME)]
        public required string cheese_name { get; set; }

        [BsonElement(CHEESE_PRICE_PER_KILO)]
        public decimal cheese_price_per_kilo { get; set; }

        [BsonElement(CHEESE_PRICE_PER_POUND)]
        public decimal cheese_price_per_pound {  get; set; } 

        [BsonElement(CHEESE_COLOR)]
        public required string cheese_color { get; set; }

        [BsonElement(CHEESE_IMAGE_URL)]
        public required string cheese_image_url { get; set; }

        [BsonElement(CHEESE_DESCRIPTION)]
        public required string cheese_description { get; set; }

        [BsonElement(CHEESE_ORIGIN)]
        public required string cheese_origin { get; set; }   

        [BsonElement(CHEESE_STOCK)]
        public required string cheese_stock { get; set; }

        [BsonElement(CREATED_AT)]
        public string created_at { get; set; } = DateTime.UtcNow.ToString();

        [BsonElement(UPDATED_AT)]
        public string updated_at { get; set; } = DateTime.UtcNow.ToString();
    }
}
