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
    public interface IBookingSeatRepository
    {

    }
    public class BookingSeatRepository : GenericRepository<BookingSeat>, IBookingSeatRepository
    {
        public BookingSeatRepository(TicketBookingDbContext context) : base(context)
        {
        }
    }
}
