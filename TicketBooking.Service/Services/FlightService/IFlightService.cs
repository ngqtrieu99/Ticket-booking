using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Common.AppExceptions;
using TicketBooking.Model.DataModel;
using TicketBooking.Service.Models;
using TicketBooking.Data.Repository;

namespace TicketBooking.Service.Services.FlightService
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightViewModel>> GetFlightAsync();
        Task<FlightViewModel> GetFlightAsync(Guid id);
        Task<int> UpdateFlightAsync(FlightUpdateModel flightUpdateModel);
        Task<Guid> InsertAsync(FlightRequestModel flightRequestModel);
        Task<string> RemoveAsync(Guid id);
        Task<bool> UpdateFlightSeat(Guid flightId, SeatClassType type, int number);
        Task<IEnumerable<FlightViewModel>> GetFlightAsync(FlightRequest flightModel);
        Task<IEnumerable<FlightViewModel>> GetFlightPagingAsync(FlightRequest request);
        Task<string> DeactiveFlightStatus(Guid flightId, bool status = false);
    }
}
