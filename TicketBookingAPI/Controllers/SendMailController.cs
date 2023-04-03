using Microsoft.AspNetCore.Mvc;
using TicketBooking.Service.Models;
using TicketBooking.Service.Services.SendMailService;

namespace TicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly ISendMailService mailService;
        public SendMailController(ISendMailService sendMail)
        {
            this.mailService = sendMail;
        }
        [HttpPost("send-mail")]
        public async Task<IActionResult> SendMail([FromBody] MailRequest request)
        {
            await mailService.SendEmailAsync(request);
            return Ok();
        }
    }
}
