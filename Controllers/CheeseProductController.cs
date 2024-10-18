﻿using Microsoft.AspNetCore.Mvc;
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

        private readonly CheeseService _cheese_product_service;

        public CheeseProductController(CheeseService cheese_product_service)
        {
            _cheese_product_service = cheese_product_service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CheeseProductModel>>> Get() =>
            await _cheese_product_service.GetAllAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CheeseProductModel>> GetCheeseProductById(string id)
        {
            try
            {
                var cheese_product_id = await _cheese_product_service.GetByIdAsync(id) ?? 
                    throw new GetCheeseProductByIdException($"Cheese product with id {id} not found.");

                if (id is null)
                {
                    throw new GetCheeseProductByIdException($"Cheese product id param {id} is null.");
                }

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


        [HttpPost]
        public async Task<IActionResult> PostCheeseProduct([FromBody] CheeseProductDto cheese_product_dto)
        {
            try
            {
                var new_cheese_product_dto = CheeseModelMapper.ToModel(cheese_product_dto);

                await _cheese_product_service.CreateAsync(new_cheese_product_dto);

                if (new_cheese_product_dto is null)
                {
                    throw new PostCheeseProductException($"Cheese product body {new_cheese_product_dto} is null.");
                }

                return CreatedAtAction(nameof(Get), new { new_cheese_product_dto.id }, new_cheese_product_dto);
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
        public async Task<IActionResult> UpdateCheeseProduct(string id, [FromBody] CheeseProductDto updated_cheese_product_dto)
        {
            try
            {
                var new_updated_cheese_product_dto = CheeseModelMapper.ToModel(updated_cheese_product_dto);

                var cheese_product_id = await _cheese_product_service.GetByIdAsync(id) ?? 
                    throw new UpdateCheeseProductException($"Cheese product with id {id} not found.");

                if (updated_cheese_product_dto is null)
                {
                    throw new UpdateCheeseProductException($"Cheese product body {updated_cheese_product_dto} is null.");
                }

                new_updated_cheese_product_dto.id = cheese_product_id.id;

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

        [HttpDelete("{id:length(24)}")]
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
