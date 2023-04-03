using TicketBooking.Data.DataModel;
using TicketBooking.Data.DbContext;
using TicketBooking.Data.Infrastructure;

namespace TicketBooking.Data.Repository
{
    public interface ISeatRepository : IRepository<Seat>
    {
            
    }
    
    public class SeatRepository : GenericRepository<Seat>, ISeatRepository
    {
        public SeatRepository(TicketBookingDbContext context) : base(context)
        {
            
        }
    }
}