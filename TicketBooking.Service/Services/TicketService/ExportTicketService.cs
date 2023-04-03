using TicketBooking.Data.DataModel;
using TicketBooking.Data.Infrastructure;
using TicketBooking.Data.Repository;
using TicketBooking.Service.Models;

namespace TicketBooking.Service.Services.TicketService
{
    public class ExportTicketService : IExportTicket
    {
        
        private readonly ITicketRepository ticketRepo;
        private readonly IFlightRepository flightRepo;
        private readonly IPassengerRepository passengerRepo;
        private readonly IBookingRepository bookingRepo;
        private readonly IBookingListRepository bookingListRepo;
        private IUnitOfWork unitOfWork;
        public ExportTicketService(IUnitOfWork unitOfWork, 
            ITicketRepository ticketRepo, 
            IFlightRepository flightRepo, 
            IPassengerRepository passengerRepo, 
            IBookingRepository bookingRepo, 
            IBookingListRepository bookingListRepo)
        {
            this.unitOfWork = unitOfWork;
            this.ticketRepo = ticketRepo;
            this.flightRepo = flightRepo;
            this.passengerRepo = passengerRepo;
            this.bookingRepo = bookingRepo;
            this.bookingListRepo = bookingListRepo;
        }

    }
}
