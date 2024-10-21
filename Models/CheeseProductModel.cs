using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PZCheeseriaRestApi.Models
{
    /// <summary>
    /// Represents a cheese product model stored in the MongoDB database.
    /// </summary>
    public class CheeseProductModel
    {
        // Constant field names for BsonElement attributes
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

        /// <summary>
        /// Gets or sets the unique identifier for the cheese product.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string _id { get; set; }

        /// <summary>
        /// Gets or sets the name of the cheese product.
        /// </summary>
        [BsonElement(CHEESE_PRODUCT_NAME)]
        public required string cheese_product_name { get; set; }

        /// <summary>
        /// Gets or sets the price of the cheese product per kilo.
        /// </summary>
        [BsonElement(CHEESE_PRODUCT_PRICE_PER_KILO)]
        public required double cheese_product_price_per_kilo { get; set; }

        /// <summary>
        /// Gets or sets the color of the cheese product.
        /// </summary>
        [BsonElement(CHEESE_PRODUCT_COLOR)]
        public required string cheese_product_color { get; set; }

        /// <summary>
        /// Gets or sets the image URL of the cheese product.
        /// </summary>
        [BsonElement(CHEESE_PRODUCT_IMAGE_URL)]
        public required string cheese_product_image_url { get; set; }

        /// <summary>
        /// Gets or sets the description of the cheese product.
        /// </summary>
        [BsonElement(CHEESE_PRODUCT_DESCRIPTION)]
        public required string cheese_product_description { get; set; }

        /// <summary>
        /// Gets or sets the origin of the cheese product.
        /// </summary>
        [BsonElement(CHEESE_PRODUCT_ORIGIN)]
        public required string cheese_product_origin { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity of the cheese product.
        /// </summary>
        [BsonElement(CHEESE_PRODUCT_STOCK)]
        public required int cheese_product_stock { get; set; }

        /// <summary>
        /// Gets or sets the creation date and time of the cheese product.
        /// </summary>
        [BsonElement(CREATED_AT)]
        public string created_at { get; set; } = DateTime.UtcNow.ToString();

        /// <summary>
        /// Gets or sets the last updated date and time of the cheese product.
        /// </summary>
        [BsonElement(UPDATED_AT)]
        public string updated_at { get; set; } = DateTime.UtcNow.ToString();
    }
}

