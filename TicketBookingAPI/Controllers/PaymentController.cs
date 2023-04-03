using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Service.Models;
using TicketBooking.Service.Services.BookingService;
using TicketBooking.Service.Services.Payment;

namespace TicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        private readonly ILogger<BookingController> logger;
        public PaymentController(IPaymentService paymentService, ILogger<BookingController> logger)
        {
            this.paymentService = paymentService;
            this.logger = logger;
        }

        [HttpPost("request-payment")]
        public async Task<IActionResult> CreatePaymentUrl(PaymentInformationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            logger.LogInformation("Start Payment");
            var url = await paymentService.CreatePaymentUrl(model, HttpContext);
            if (url != null)
                return StatusCode(StatusCodes.Status201Created, url);

            logger.LogError("Payment failed");
            return BadRequest(url);
        }

        [HttpGet("callback-payment")]
        public async Task<IActionResult> PaymentCallback()
        {
            logger.LogInformation("Start save bill");
            var response = await paymentService.PaymentExecute(Request.Query);

            return Redirect("http://localhost:5173/success");
        }
    }
}
