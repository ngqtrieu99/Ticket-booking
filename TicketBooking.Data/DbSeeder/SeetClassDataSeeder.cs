using Microsoft.Extensions.Logging;
using TicketBooking.Data.DbContext;
using TicketBooking.Data.DataModel;

namespace TicketBooking.Data.DbSeeder;

public class SeatClassDataSeeder : ISeatClassDataSeeder
{
    private readonly ILogger<SeatClassDataSeeder> logger;
    private readonly TicketBookingDbContext dbContext;

    public SeatClassDataSeeder(ILogger<SeatClassDataSeeder> logger, TicketBookingDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    public void InitDataBase()
    {
        if (!dbContext.SeatClasses.Any())
        {
            var seatclass = new List<SeatClass>()
            {
                new SeatClass()
                {
                    Id = Guid.NewGuid(),
                    SeatName = "Business",
                    Description = "Come with extra services"
                },

                new SeatClass()
                {
                    Id = Guid.NewGuid(),
                    SeatName = "Economy",
                    Description = "Good for basic needs"
                }
            };
            dbContext.SeatClasses.AddRange(seatclass);
        }
    }
}