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
    public interface IPassengerRepository : IRepository<Passenger>
    {
        Task<bool> AddPassenger(Passenger passenger);
    }
    public class PassengerRepository : GenericRepository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(TicketBookingDbContext context) : base(context)
        {
        }

        public async Task<bool> AddPassenger(Passenger passenger)
        {
            return await Add(passenger);
        }
    }
}
