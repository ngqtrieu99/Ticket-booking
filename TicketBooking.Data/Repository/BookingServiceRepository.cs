using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data.DbContext;
using TicketBooking.Data.Infrastructure;
using TicketBooking.Model.DataModel;

namespace TicketBooking.Data.Repository
{
    public interface IBookingServiceRepository : IRepository<BookingExtraService>
    {
    }
    public class BookingServiceRepository : GenericRepository<BookingExtraService>, IBookingServiceRepository
    {
        public BookingServiceRepository(TicketBookingDbContext context) : base(context)
        {
           
        }

    }
}
