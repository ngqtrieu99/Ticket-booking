using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketBooking.Data.DataModel;
using TicketBooking.Data.DbContext;
using TicketBooking.Data.Infrastructure;
using TicketBooking.Model.DataModel;

namespace TicketBooking.Data.Repository
{
    public interface IFlightRepository : IRepository<Flight>
    {
        Task<IEnumerable<Flight>> GetAllFlight();

        Task<Flight> GetFlightById(Guid Id);

        Task<IEnumerable<Flight>> GetFlightByRequest(FlightRequest flightrequest);
        
        Task<IEnumerable<Flight>> GetFlightPagingByRequest(FlightRequest request);
    }

    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        public FlightRepository(TicketBookingDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Flight>> GetAllFlight()
        {
            var query = from f in _context.Flights
                join ar in _context.Aircrafts on f.AircraftId equals ar.Id
                join fs in _context.FlightSchedules on f.ScheduleId equals fs.Id
                select f;
            
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetFlightByRequest(FlightRequest flightrequest)
        {
            var convertedDate = flightrequest.DepartDate.Date;

            var departGUID = await (from a in _context.Airports
                                    where a.Code == flightrequest.DepartCode
                                    select a.Id).ToListAsync();

            var arrivalGUID = await (from a in _context.Airports
                                     where a.Code == flightrequest.ArrivalCode
                                     select a.Id).ToListAsync();

            var query = from f in _context.Flights
                        join fs in _context.FlightSchedules on f.ScheduleId equals fs.Id
                        join ar in _context.Aircrafts on f.AircraftId equals ar.Id
                        where ((DateTime.Compare(fs.DepartureTime.Date, convertedDate) == 0)
                               && (departGUID.Contains(fs.DepartureAirportId))
                                   && (arrivalGUID.Contains(fs.ArrivalAirportId))
                                   && f.IsFlightActive == true)
                        select f;

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetFlightPagingByRequest(FlightRequest request)
        {
            var convertedDate = request.DepartDate.Date;

            var departGUID = await (from a in _context.Airports
                                    where a.Code == request.DepartCode
                                    select a.Id).ToListAsync();

            var arrivalGUID = await (from a in _context.Airports
                                     where a.Code == request.ArrivalCode
                                     select a.Id).ToListAsync();

            var query = (from f in _context.Flights
                        join fs in _context.FlightSchedules on f.ScheduleId equals fs.Id
                        join ar in _context.Aircrafts on f.AircraftId equals ar.Id
                        where ((DateTime.Compare(fs.DepartureTime.Date, convertedDate) == 0)
                               && (departGUID.Contains(fs.DepartureAirportId))
                                   && (arrivalGUID.Contains(fs.ArrivalAirportId))
                                   && f.IsFlightActive == true)
                        select f).Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

            return await query.ToListAsync();
        }
        
        public async Task<Flight> GetFlightById(Guid Id)
        {
            var query = (from f in _context.Flights
                        join fs in _context.FlightSchedules on f.ScheduleId equals fs.Id
                        join ar in _context.Aircrafts on f.AircraftId equals ar.Id
                        where (f.Id == Id)
                        select f).FirstOrDefault();

            return query;
        }
    }
}
