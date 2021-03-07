using Microsoft.AspNetCore.Mvc;
using PaymentProcessorAPI.Models;
using PaymentProcessorAPI.Services;

namespace PaymentProcessorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {

        readonly GatewayService _gatewayService;
        public PaymentsController(GatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }


        [HttpPost]
        public IActionResult Post(Request request)
        {
            if (_gatewayService.Validate(request))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _gatewayService.ProcessPayment(request);
                    }
                    catch
                    {
                        return StatusCode(500);
                    }
                    return StatusCode(200);
                }
                else
                    return StatusCode(400);
            }
            else
                return StatusCode(400);
        }
    }
}
