using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data;
using TicketBooking.Data.Infrastructure;
using TicketBooking.Data.Repository;
using TicketBooking.Service.Models;

namespace TicketBooking.Service.Services.FlightScheService
{
    public interface IFlightScheServices
    {
        Task<IEnumerable<FlightScheViewModel>> GetFlightScheAsync();
        Task<FlightScheViewModel> GetFlightScheAsync(Guid id);
        Task<string> UpdateFlightScheAsync(FlightScheViewModel flightScheViewModel);
        Task<string> InsertAsync(FlightScheViewModel flightScheViewModel);
        Task<string> RemoveAsync(Guid id);
    }
}
