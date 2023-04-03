using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketBooking.Data.DbContext;
using TicketBooking.Data.Infrastructure;
using TicketBooking.Data.DataModel;

namespace TicketBooking.Data.Repository
{
    public interface IAirportRepository : IRepository<Airport>
    {
        Task<Airport> GetByAirportCode(string code);
    }

    public class AirportRepository : GenericRepository<Airport>, IAirportRepository
    {
        public AirportRepository(TicketBookingDbContext context) : base(context)
        {
    
        }

        public async Task<Airport> GetByAirportCode(string code)
        {
            var query = await (from a in _context.Airports
                        where a.Code == code
                        select a).ToListAsync();

            return query.FirstOrDefault();
        }
    }
}
