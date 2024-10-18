using PZCheeseriaRestApi.Models.Dto;
using PZCheeseriaRestApi.Models;

namespace PZCheeseriaRestApi.Services
{
    public static class CheeseProductModelMapper
    {
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

        public static CheeseProductModel ToModel(CheeseProductDto cheese_dto)
        {
            return new CheeseProductModel
            {
                id = string.Empty,

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
