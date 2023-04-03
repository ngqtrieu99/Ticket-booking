using TicketBooking.Data.DbContext;

namespace TicketBooking.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TicketBookingDbContext _context;

        public UnitOfWork(TicketBookingDbContext context)
        { _context = context; }

        public async Task CompletedAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}