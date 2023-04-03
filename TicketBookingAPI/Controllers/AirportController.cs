using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Service.Models;
using TicketBooking.Service.Services.AircraftService;
using TicketBooking.Service.Services.AirportService;
using TicketBooking.Service.Services.BookingService;

namespace TicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private IAirportService airportService { get; }
        private readonly ILogger<AirportController> logger;
        public AirportController(IAirportService airportService, ILogger<AirportController> logger)
        {
            this.airportService = airportService;
            this.logger = logger;
        }

        [HttpGet("airports")]
        public async Task<IActionResult> GetAllAirport()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            logger.LogInformation("Start get");
            var result = await airportService.GetAirportAsync();

            if (result != null)
                return StatusCode(StatusCodes.Status201Created, result);

            logger.LogError("get failed");
            return BadRequest(result);
        }

    }
}
