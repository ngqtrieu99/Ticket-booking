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
    public interface IBookingListRepository : IRepository<BookingList>
    {
        IEnumerable<BookingList> GetBookingList(Guid? bookingId);
    }
    public class BookingListRepository : GenericRepository<BookingList>, IBookingListRepository
    {
        public BookingListRepository(TicketBookingDbContext context) : base(context)
        {
        }

        public IEnumerable<BookingList> GetBookingList(Guid? bookingId)
        {
            return Find(x => x.BookingId == bookingId);
        }
    }
}
