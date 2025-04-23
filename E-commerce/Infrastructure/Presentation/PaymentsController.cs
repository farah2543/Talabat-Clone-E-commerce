using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOs;

namespace Presentation
{
    public class PaymentsController (IServiceManager serviceManager):ApiController
    {
        [HttpPost("{basketId}")]

        public async Task<ActionResult<BasketDTO>>CreateOrUpdatePaymentIntent(string basketId)
        {
            var result = await serviceManager.paymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(result);
        }

    }
}
