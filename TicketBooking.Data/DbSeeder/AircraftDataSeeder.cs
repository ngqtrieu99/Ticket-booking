using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketBooking.Data.DbContext;
using TicketBooking.Data.DataModel;

namespace TicketBooking.Data.DbSeeder
{
    public class AircraftDataSeeder : IAircraftDataSeeder
    {
        private readonly ILogger<AircraftDataSeeder> logger;
        private readonly TicketBookingDbContext dbContext;

        public AircraftDataSeeder(ILogger<AircraftDataSeeder> _logger, TicketBookingDbContext context)
        {
            logger = _logger;
            dbContext = context;
        }



        public void InitDataBase()
        {
            if (!dbContext.Aircrafts.Any())
            {
                var aircraft = new List<Aircraft>()
                {
                    new Aircraft
                    {
                        Id = Guid.Parse("f222243c-d1a2-4ecd-a7a4-b583f38f8b37"),
                        Model = "An-24",
                        Manufacture = "Antono",
                        NumRowSeat = 30,
                        NumColumnSeat = 6,
                        NumRowBusiness = 5,
                        NumRowEconomy = 25
                    },

                    new Aircraft
                    {
                        Id = Guid.Parse("10a61395-2694-4cb7-861c-f6b98d969171"),
                        Model = "A330",
                        Manufacture = "Airbus",
                        NumRowSeat = 25,
                        NumColumnSeat = 6,
                        NumRowBusiness = 6,
                        NumRowEconomy = 19
                    },

                    new Aircraft
                    {
                        Id = Guid.Parse("fdf25740-a299-44e3-91a6-33bf5cd51323"),
                        Model = "747",
                        Manufacture = "Boeing",
                        NumRowSeat = 35,
                        NumColumnSeat = 6,
                        NumRowBusiness = 5,
                        NumRowEconomy = 30
                    },

                    new Aircraft
                    {
                        Id = Guid.Parse("f74f8452-755d-4d3e-91af-a97730e746c7"),
                        Model = "E-JET",
                        Manufacture = "Embrar",
                        NumRowSeat = 40,
                        NumColumnSeat = 6,
                        NumRowBusiness = 10,
                        NumRowEconomy = 30
                    },

                    new Aircraft
                    {
                        Id = Guid.Parse("394db54f-273b-487f-93ec-8f5a1e375ebc"),
                        Model = "727",
                        Manufacture = "Boeing",
                        NumRowSeat = 40,
                        NumColumnSeat = 6,
                        NumRowBusiness = 5,
                        NumRowEconomy = 25
                    }
                };
                dbContext.Aircrafts.AddRange(aircraft);
            }
        }
    }
}
