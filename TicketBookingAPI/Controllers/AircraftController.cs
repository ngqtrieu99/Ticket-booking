using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TicketBooking.Data;
using TicketBooking.Data.DbContext;

using TicketBooking.Service.Services.AircraftService;
using TicketBooking.Service.Models;

namespace TicketBookingAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        private IAircraftSerivce AircraftService { get; }

        public AircraftController(IAircraftSerivce AircraftService)
        {
            this.AircraftService = AircraftService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAircraft(AircraftViewModel aircraftModel)
        {
            if (aircraftModel == null)
            {
                return NotFound();
            }

            if (aircraftModel.Model?.Length > 6 || aircraftModel.Manufacture?.Length > 10)
            {
                return NotFound();
            }
            
            return Accepted(await AircraftService.InsertAsync(aircraftModel));
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveAircraft(Guid id)
        {
            await AircraftService.RemoveAsync(id);
            return Accepted();
        }

        [HttpPut]

        public async Task<ActionResult> UpdateAircraft(AircraftViewModel aircraftModel)
        {
            await AircraftService.UpdateAircraftAsync(aircraftModel);
            return Accepted();
        }

        [HttpGet("aircrafts")]
        public async Task<ActionResult> GetAircraft() => Ok(await AircraftService.GetAircraftAsync());

        [HttpGet("aircraft/{id}")]
        public async Task<ActionResult> GetAircraftbyId(Guid id) => Ok(await AircraftService.GetAircraftAsync(id));
    }
}