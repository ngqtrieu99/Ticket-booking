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
    public interface ITicketRepository: IRepository<Ticket>
    {
        Task<bool> AddTicket(Ticket ticket);
    }
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(TicketBookingDbContext context) : base(context)
        {

        }

        public async Task<bool> AddTicket(Ticket ticket)
        {
            return await Add(ticket);
        }
    }
}
