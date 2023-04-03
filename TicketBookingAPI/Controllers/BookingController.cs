using Microsoft.AspNetCore.Mvc;
using TicketBooking.Service.Models;
using TicketBooking.Service.Services.BookingService;
using TicketBooking.Service.Services.ContactDetailService;

namespace TicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;
        private readonly IContactDetailServcie contactService;
        private readonly ILogger<BookingController> logger;

        public BookingController(IBookingService bookingService, IContactDetailServcie contactService, ILogger<BookingController> logger)
        {
            this.bookingService = bookingService;
            this.contactService = contactService;
            this.logger = logger;
        }

        [HttpPost("request-booking")]
        public async Task<IActionResult> RequestBooking([FromBody]BookingRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            logger.LogInformation("Start Booking");
            var result = await bookingService.RequestBooking(model);

            if (result != null)
                return StatusCode(StatusCodes.Status201Created, result);

            logger.LogError("Booking failed");
            return BadRequest(result);
        }


        [HttpPost("cancel-booking")]
        public async Task<IActionResult> CancelBooking(Guid bookingId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            logger.LogInformation("Cancel booking");
            var result = await bookingService.CancelBooking(bookingId);

            if (result.Status == true)
                return StatusCode(StatusCodes.Status200OK, result);

            logger.LogError("Cancel failed");
            return BadRequest(result);
        }

        [HttpGet("get-booking")]
        public async Task<IActionResult> GetByBookingCode(string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            logger.LogInformation("Search booking");
            var result = await bookingService.GetByBookingCode(code);

            if (result.Status == true)
                return StatusCode(StatusCodes.Status200OK, result);

            logger.LogError("Search failed");
            return Ok(result);
        }

        [HttpPost("request-contact")]
        public async Task<IActionResult> CreateContact([FromBody] ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                var result = await contactService.CreateContactInfo(contact);
                if (result != null)
                {
                    return Ok(result);
                }

                return BadRequest("Create failed");

            }
            return BadRequest(ModelState);
        }
        [HttpGet("get-service")]
        public async Task<IActionResult> GetService()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            logger.LogInformation("get service");
            var result = await bookingService.GetService();

            if (result != null)
                return StatusCode(StatusCodes.Status200OK, result);

            logger.LogError("Get failed");
            return BadRequest(result);
        }
    }
}
