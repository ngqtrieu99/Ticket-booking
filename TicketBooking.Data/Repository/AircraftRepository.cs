using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data.DataModel;
using TicketBooking.Data.DbContext;
using TicketBooking.Data.Infrastructure;
using TicketBooking.Data.DataModel;

namespace TicketBooking.Data.Repository
{
    public interface IAircraftRepository : IRepository<Aircraft>
    {

    }

    public class AircraftRepository : GenericRepository<Aircraft>, IAircraftRepository
    {
        public AircraftRepository(TicketBookingDbContext context) : base(context)
        {

        } 
    }
}
