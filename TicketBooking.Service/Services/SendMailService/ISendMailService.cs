using TicketBooking.Service.Models;

namespace TicketBooking.Service.Services.SendMailService
{
    public interface ISendMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
