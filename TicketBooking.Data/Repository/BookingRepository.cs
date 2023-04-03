using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data.DataModel;
using TicketBooking.Data.DbContext;
using TicketBooking.Data.Infrastructure;

namespace TicketBooking.Data.Repository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<bool> AddBooking(Booking model);
    }
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(TicketBookingDbContext context) : base(context)
        {
        }

        public async Task<bool> AddBooking(Booking model)
        {
            return await Add(model);
        }
    }
}
