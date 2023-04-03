using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketBooking.Data.DataModel;
using TicketBooking.Data.DbContext;

namespace TicketBooking.Data.DbSeeder
{
    public class AirportDataSeeder : IAirportDataSeeder
    {
        private readonly ILogger<AirportDataSeeder> logger;
        private readonly TicketBookingDbContext dbContext;

        public AirportDataSeeder(ILogger<AirportDataSeeder> _logger, TicketBookingDbContext context)
        {
            logger = _logger;
            dbContext = context;
        }

        public void InitDataBase()
        {
            if (!dbContext.Airports.Any())
            {
                var airport = new List<Airport>()
                {
                    new Airport()
                    {
                        Id = Guid.Parse("8e204406-9d1b-45da-a643-2ba07ff9fca5"),
                        Name = "Tan Son Nhat",
                        City = "Ho Chi Minh",
                        Country = "Viet Nam",
                        Code = "SGN"
                    },

                    new Airport()
                    {
                        Id = Guid.Parse("7fc1f3c4-1244-4f70-b663-cf0e322a523e"),
                        Name = "Noi Bai",
                        City = "Ha Noi",
                        Country = "Viet Nam",
                        Code = "HAN"
                    },

                    new Airport()
                    {
                        Id = Guid.Parse("35520915-26eb-4dd0-acad-6deb323c2c5f"),
                        Name = "Phu Cat",
                        City = "Binh Dinh",
                        Country = "Viet Nam",
                        Code = "UIH"
                    }
                };
                dbContext.Airports.AddRange(airport);
            }
        }
    }
}
