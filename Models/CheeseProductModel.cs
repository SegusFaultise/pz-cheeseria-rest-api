using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace PZCheeseriaRestApi.Models
{
    public class CheeseProductModel
    {
        const string CHEESE_PRODUCT_NAME = "cheese_product_name";
        const string CHEESE_PRODUCT_PRICE_PER_KILO = "cheese_product_price_per_kilo";
        const string CHEESE_PRODUCT_PRICE_PER_POUND = "cheese_product_price_per_pound";
        const string CHEESE_PRODUCT_COLOR = "cheese_product_color";
        const string CHEESE_PRODUCT_IMAGE_URL = "cheese_product_image_url";
        const string CHEESE_PRODUCT_DESCRIPTION = "cheese_product_description";
        const string CHEESE_PRODUCT_ORIGIN = "cheese_product_origin";
        const string CHEESE_PRODUCT_STOCK = "cheese_product_stock";

        const string CREATED_AT = "created_at";
        const string UPDATED_AT = "updated_at";

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string id { get; set; }

        [BsonElement(CHEESE_PRODUCT_NAME)]
        public required string cheese_product_name { get; set; }

        [BsonElement(CHEESE_PRODUCT_PRICE_PER_KILO)]
        public required double cheese_product_price_per_kilo { get; set; }

        [BsonElement(CHEESE_PRODUCT_COLOR)]
        public required string cheese_product_color { get; set; }

        [BsonElement(CHEESE_PRODUCT_IMAGE_URL)]
        public required string cheese_product_image_url { get; set; }

        [BsonElement(CHEESE_PRODUCT_DESCRIPTION)]
        public required string cheese_product_description { get; set; }

        [BsonElement(CHEESE_PRODUCT_ORIGIN)]
        public required string cheese_product_origin { get; set; }   

        [BsonElement(CHEESE_PRODUCT_STOCK)]
        public required int cheese_product_stock { get; set; }

        [BsonElement(CREATED_AT)]
        public string created_at { get; set; } = DateTime.UtcNow.ToString();

        [BsonElement(UPDATED_AT)]
        public string updated_at { get; set; } = DateTime.UtcNow.ToString();
    }
}
