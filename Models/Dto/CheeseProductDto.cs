using MongoDB.Bson.Serialization.Attributes;

namespace PZCheeseriaRestApi.Models.Dto
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a cheese product.
    /// </summary>
    public class CheeseProductDto
    {
        /// <summary>
        /// Gets or sets the name of the cheese product.
        /// </summary>
        public required string cheese_product_name { get; set; }

        /// <summary>
        /// Gets or sets the price of the cheese product per kilo.
        /// </summary>
        public double cheese_product_price_per_kilo { get; set; }

        /// <summary>
        /// Gets or sets the color of the cheese product.
        /// </summary>
        public required string cheese_product_color { get; set; }

        /// <summary>
        /// Gets or sets the image URL of the cheese product.
        /// </summary>
        public required string cheese_product_image_url { get; set; }

        /// <summary>
        /// Gets or sets the description of the cheese product.
        /// </summary>
        public required string cheese_product_description { get; set; }

        /// <summary>
        /// Gets or sets the origin of the cheese product.
        /// </summary>
        public required string cheese_product_origin { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity of the cheese product.
        /// </summary>
        public required int cheese_product_stock { get; set; }
    }
}

