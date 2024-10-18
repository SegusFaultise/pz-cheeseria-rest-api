using Microsoft.AspNetCore.Mvc;
using PZCheeseriaRestApi.Exceptions;
using PZCheeseriaRestApi.Models;
using PZCheeseriaRestApi.Services;


namespace PZCheeseriaRestApi.Controllers
{
    [ApiController]
    [Route("pz-cheeseria-rest-api/[controller]")]
    public class CheeseProductController : ControllerBase
    {
        const int SEVER_ERROR_RESPONSE_CODE = 500;
        const string SERVER_ERROR_RESPONSE_MESSAGE = "An unexpected error occurred: ";

        private readonly CheeseService _cheese_service;

        public CheeseProductController(CheeseService cheese_service)
        {
            _cheese_service = cheese_service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CheeseModel>>> Get() =>
            await _cheese_service.GetAllAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CheeseModel>> GetCheeseProductById([FromBody] string id)
        {
            try
            {
                var cheese = await _cheese_service.GetByIdAsync(id) ?? 
                    throw new GetCheeseProductByIdException($"Cheese product with id {id} not found.");

                if (id is null)
                {
                    throw new GetCheeseProductByIdException($"Cheese product id param {id} is null.");
                }

                return Ok(cheese);
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


        [HttpPost]
        public async Task<IActionResult> PostCheeseProduct([FromBody] CheeseModel new_cheese)
        {
            try
            {
                await _cheese_service.CreateAsync(new_cheese);

                if (new_cheese is null)
                {
                    throw new PostCheeseProductException($"Cheese product body {new_cheese} is null.");
                }

                return CreatedAtAction(nameof(Get), new { new_cheese.id }, new_cheese);
            } catch (PostCheeseProductException post_cheese_exception)
            {
                return BadRequest(post_cheese_exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(SEVER_ERROR_RESPONSE_CODE, SERVER_ERROR_RESPONSE_MESSAGE + exception.Message);
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateCheeseProduct([FromBody] string id, CheeseModel updated_cheese_model)
        {
            try
            {
                var cheese = await _cheese_service.GetByIdAsync(id) ?? 
                    throw new UpdateCheeseProductException($"Cheese product with id {id} not found.");

                if (updated_cheese_model is null)
                {
                    throw new UpdateCheeseProductException($"Cheese product body {updated_cheese_model} is null.");
                }

                updated_cheese_model.id = cheese.id;

                await _cheese_service.UpdateAsync(id, updated_cheese_model);

                return NoContent();
            } catch (UpdateCheeseProductException update_cheese_product_exception)
            {
                return BadRequest(update_cheese_product_exception.Message);
            } catch (Exception exception)
            {
                return StatusCode(SEVER_ERROR_RESPONSE_CODE, SERVER_ERROR_RESPONSE_MESSAGE + exception.Message);
            }
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteCheeseProduct([FromBody] string id)
        {
            try
            {
                var cheese = await _cheese_service.GetByIdAsync(id) ??
                    throw new DeleteCheeseProductException($"Cheese product with id {id} not found.");

                if (id is null)
                {
                    throw new DeleteCheeseProductException($"Cheese product id {id} is null.");
                }

                await _cheese_service.DeleteAsync(id);

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
