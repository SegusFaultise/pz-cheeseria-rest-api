using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PZCheeseriaRestApi.Exceptions;
using PZCheeseriaRestApi.Models;
using PZCheeseriaRestApi.Models.Dto;
using PZCheeseriaRestApi.Services;


namespace PZCheeseriaRestApi.Controllers
{
    [ApiController]
    [Route("pz-cheeseria-rest-api/[controller]")]
    public class CheeseProductController : ControllerBase
    {
        const int SEVER_ERROR_RESPONSE_CODE = 500;
        const string SERVER_ERROR_RESPONSE_MESSAGE = "An unexpected error occurred: ";

        private readonly CheeseProductService _cheese_product_service;

        public CheeseProductController(CheeseProductService cheese_product_service)
        {
            _cheese_product_service = cheese_product_service;
        }

        [EnableCors]
        [HttpGet]
        [Route("GetAllCheeseProducts")]
        public async Task<ActionResult<List<CheeseProductModel>>> GetAllCheeseProducts() =>
            await _cheese_product_service.GetAllAsync();

        [EnableCors]
        [HttpGet("{id:length(24)}")]
        [Route("GetAllCheeseProductById")]
        public async Task<ActionResult<CheeseProductModel>> GetCheeseProductById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Cheese product id parameter cannot be null or empty.");
            }

            try
            {
                var cheese_product_id = await _cheese_product_service.GetByIdAsync(id) ?? 
                    throw new GetCheeseProductByIdException($"Cheese product with id {id} not found.");

                return Ok(cheese_product_id);
            }
            catch (GetCheeseProductByIdException get_cheese_by_id_exception)
            {
                return BadRequest(get_cheese_by_id_exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(SEVER_ERROR_RESPONSE_CODE, SERVER_ERROR_RESPONSE_MESSAGE + exception.Message);
            }
        }

        [EnableCors]
        [HttpGet]
        [Route("GetCheeseProductByName")]
        public async Task<ActionResult<CheeseProductModel>> GetCheeseProductByName(string cheese_product_name)
        {
            if (string.IsNullOrEmpty(cheese_product_name))
            {
                return BadRequest("Cheese product id parameter cannot be null or empty.");
            }

            try
            {
                var new_cheese_product_name = await _cheese_product_service.GetByCheeseProductNameAsync(cheese_product_name) ?? 
                    throw new GetCheeseProductByIdException($"Cheese product with name {cheese_product_name} not found.");

                return Ok(new_cheese_product_name);
            }
            catch (GetCheeseProductByIdException get_cheese_by_id_exception)
            {
                return BadRequest(get_cheese_by_id_exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(SEVER_ERROR_RESPONSE_CODE, SERVER_ERROR_RESPONSE_MESSAGE + exception.Message);
            }
        }

        [EnableCors]
        [HttpPost]
        [Route("PostCheeseProduct")]
        public async Task<IActionResult> PostCheeseProduct([FromBody] CheeseProductDto cheese_product_dto)
        {
            try
            {
                var new_cheese_product_dto = CheeseProductModelMapper.ToModel(cheese_product_dto);

                await _cheese_product_service.CreateAsync(new_cheese_product_dto);

                if (new_cheese_product_dto is null)
                {
                    throw new PostCheeseProductException($"Cheese product body {new_cheese_product_dto} is null.");
                }

                return CreatedAtAction(nameof(GetAllCheeseProducts), new { new_cheese_product_dto._id }, CheeseProductModelMapper.ToDto(new_cheese_product_dto));
            } catch (PostCheeseProductException post_cheese_exception)
            {
                return BadRequest(post_cheese_exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(SEVER_ERROR_RESPONSE_CODE, SERVER_ERROR_RESPONSE_MESSAGE + exception.Message);
            }
        }

        [EnableCors]
        [HttpPut("{id:length(24)}")]
        [Route("UpdateCheeseProduct")]
        public async Task<IActionResult> UpdateCheeseProduct(string id, [FromBody] CheeseProductDto updated_cheese_product_dto)
        {
            try
            {
                var new_updated_cheese_product_dto = CheeseProductModelMapper.ToModel(updated_cheese_product_dto);

                var cheese_product_id = await _cheese_product_service.GetByIdAsync(id) ?? 
                    throw new UpdateCheeseProductException($"Cheese product with id {id} not found.");

                if (updated_cheese_product_dto is null)
                {
                    throw new UpdateCheeseProductException($"Cheese product body {updated_cheese_product_dto} is null.");
                }

                new_updated_cheese_product_dto._id = cheese_product_id._id;

                await _cheese_product_service.UpdateAsync(id, new_updated_cheese_product_dto);

                return NoContent();
            } catch (UpdateCheeseProductException update_cheese_product_exception)
            {
                return BadRequest(update_cheese_product_exception.Message);
            } catch (Exception exception)
            {
                return StatusCode(SEVER_ERROR_RESPONSE_CODE, SERVER_ERROR_RESPONSE_MESSAGE + exception.Message);
            }
        }

        [EnableCors]
        [HttpDelete("{id:length(24)}")]
        [Route("DeleteCheeseProduct")]
        public async Task<IActionResult> DeleteCheeseProduct(string id)
        {
            try
            {
                var cheese_product_id = await _cheese_product_service.GetByIdAsync(id) ??
                    throw new DeleteCheeseProductException($"Cheese product with id {id} not found.");

                if (id is null)
                {
                    throw new DeleteCheeseProductException($"Cheese product id {id} is null.");
                }

                await _cheese_product_service.DeleteAsync(id);

                return NoContent();
            } catch (DeleteCheeseProductException delete_cheese_product_exception)
            {
                return BadRequest(delete_cheese_product_exception.Message);
            } catch (Exception exception)
            {
                return StatusCode(SEVER_ERROR_RESPONSE_CODE, SERVER_ERROR_RESPONSE_MESSAGE + exception.Message);
            }
        }
    }
}