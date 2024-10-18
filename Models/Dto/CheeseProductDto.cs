using MongoDB.Bson.Serialization.Attributes;

namespace PZCheeseriaRestApi.Models.Dto
{
    public class CheeseProductDto
    {
        public required string cheese_product_name { get; set; }

        public decimal cheese_product_price_per_kilo { get; set; }

        public required string cheese_product_color { get; set; }

        public required string cheese_product_image_url { get; set; }

        public required string cheese_product_description { get; set; }

        public required string cheese_product_origin { get; set; }

        public required string cheese_product_stock { get; set; }
    }
}
