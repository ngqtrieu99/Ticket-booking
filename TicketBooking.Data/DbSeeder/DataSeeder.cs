using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketBooking.Data.DbContext;

namespace TicketBooking.Data.DbSeeder
{
    public class DataSeeder : IDataSeeder, IAircraftDataSeeder
    {

        private readonly ILogger<DataSeeder> logger;
        private readonly TicketBookingDbContext dbContext;
        private readonly IAircraftDataSeeder aircraftDataSeeder;
        private readonly IAirportDataSeeder airportDataSeeder;
        private readonly ISeatClassDataSeeder seatClassDataSeeder;

        public DataSeeder(ILogger<DataSeeder> logger,
                          TicketBookingDbContext dbContext,
                          IAircraftDataSeeder aircraftDataSeeder,
                          IAirportDataSeeder airportDataSeeder,
                          ISeatClassDataSeeder seatClassDataSeeder)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.aircraftDataSeeder = aircraftDataSeeder;
            this.airportDataSeeder = airportDataSeeder;
            this.seatClassDataSeeder = seatClassDataSeeder;
        }
        public void InitDataBase()
        {
             // seeding data for aircraft
             aircraftDataSeeder.InitDataBase();
             
             // seeding data for airport
             airportDataSeeder.InitDataBase();
             
             // Seeding data for seat class
             seatClassDataSeeder.InitDataBase();

             dbContext.SaveChanges();

        }
    }
}
