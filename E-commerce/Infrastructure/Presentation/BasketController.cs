using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOs;

namespace Presentation
{

    public class BasketController (IServiceManager _serviceManager): ApiController
    {
        [HttpGet("{id}")]
        public async Task <ActionResult<BasketDTO>>Get(string id)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsync(id);
            return Ok(basket);
        }


        [HttpPost]
        public async Task<ActionResult<BasketDTO>> Update( BasketDTO basketDTO)
        {
            var basket = await _serviceManager.BasketService.UpdateBasketAsync(basketDTO);
            return Ok(basket);

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(string id)
        {
            await _serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();

        }


    }
}
