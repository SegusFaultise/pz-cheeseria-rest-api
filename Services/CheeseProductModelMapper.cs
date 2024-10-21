using PZCheeseriaRestApi.Models.Dto;
using PZCheeseriaRestApi.Models;

namespace PZCheeseriaRestApi.Services
{
    /// <summary>
    /// Static class for mapping between <see cref="CheeseProductModel"/> and <see cref="CheeseProductDto"/>.
    /// </summary>
    public static class CheeseProductModelMapper
    {
        /// <summary>
        /// Maps a <see cref="CheeseProductModel"/> to a <see cref="CheeseProductDto"/>.
        /// </summary>
        /// <param name="cheese_product_model">The cheese product model to map from.</param>
        /// <returns>A <see cref="CheeseProductDto"/> that represents the mapped cheese product.</returns>
        public static CheeseProductDto ToDto(CheeseProductModel cheese_product_model)
        {
            return new CheeseProductDto
            {
                cheese_product_name = cheese_product_model.cheese_product_name,
                cheese_product_description = cheese_product_model.cheese_product_description,
                cheese_product_origin = cheese_product_model.cheese_product_origin,
                cheese_product_color = cheese_product_model.cheese_product_color,
                cheese_product_price_per_kilo = cheese_product_model.cheese_product_price_per_kilo,
                cheese_product_image_url = cheese_product_model.cheese_product_image_url,
                cheese_product_stock = cheese_product_model.cheese_product_stock,
            };
        }

        /// <summary>
        /// Maps a <see cref="CheeseProductDto"/> to a <see cref="CheeseProductModel"/>.
        /// </summary>
        /// <param name="cheese_dto">The cheese product DTO to map from.</param>
        /// <returns>A <see cref="CheeseProductModel"/> that represents the mapped cheese product.</returns>
        public static CheeseProductModel ToModel(CheeseProductDto cheese_dto)
        {
            return new CheeseProductModel
            {
                _id = string.Empty, // Assigning an empty string for the ID; to be set by the database

                cheese_product_name = cheese_dto.cheese_product_name,
                cheese_product_color = cheese_dto.cheese_product_color,
                cheese_product_description = cheese_dto.cheese_product_description,
                cheese_product_image_url = cheese_dto.cheese_product_image_url,
                cheese_product_origin = cheese_dto.cheese_product_origin,
                cheese_product_stock = cheese_dto.cheese_product_stock,
                cheese_product_price_per_kilo = cheese_dto.cheese_product_price_per_kilo,
            };
        }
    }
}