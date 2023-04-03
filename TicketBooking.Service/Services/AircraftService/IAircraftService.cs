using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data;
using TicketBooking.Data.Infrastructure;
using TicketBooking.Data.Repository;
using TicketBooking.Service.Models;

namespace TicketBooking.Service.Services.AircraftService
{
    public interface IAircraftSerivce
    {
        Task<IEnumerable<AircraftViewModel>> GetAircraftAsync();
        Task<AircraftViewModel> GetAircraftAsync(Guid id);
        Task UpdateAircraftAsync(AircraftViewModel aircraftDto);
        Task<string> InsertAsync(AircraftViewModel aircraftDto);
        Task<bool> RemoveAsync(Guid id);
    }
}
