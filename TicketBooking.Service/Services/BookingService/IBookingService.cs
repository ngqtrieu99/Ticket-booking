using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data.DataModel;
using TicketBooking.Model.DataModel;
using TicketBooking.Service.Models;

namespace TicketBooking.Service.Services.BookingService
{
    public interface IBookingService
    {
        Task<string> RequestBooking(BookingRequestModel model);
        Task<Response> CancelBooking(Guid bookingId);
        Task<Response> GetByBookingCode(string bookingCode);
        Task<IEnumerable<ExtraService>> GetService();

    }
}
