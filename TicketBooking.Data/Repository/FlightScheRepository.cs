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
    public interface IFlightScheRepository : IRepository<FlightSchedule>
    {

    }

    public class FlightScheRepository : GenericRepository<FlightSchedule>, IFlightScheRepository
    {
        public FlightScheRepository(TicketBookingDbContext context) : base(context)
        {

        }
    }
}
