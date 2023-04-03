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
    public interface IFlightScheduleRepository : IRepository<FlightSchedule>
    {
    }
    public class FlightScheduleRepository : GenericRepository<FlightSchedule>, IFlightScheduleRepository
    {
        public FlightScheduleRepository(TicketBookingDbContext context) : base(context)
        {
        }
    }
}
