using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data.DataModel;
using TicketBooking.Service.Models;

namespace TicketBooking.Service.Services.BookingListService
{
    public interface IBookingListService
    {
        Task<bool> AddBookingList(BookingListViewModel bookingList);
    }
}
