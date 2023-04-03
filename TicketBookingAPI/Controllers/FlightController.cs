using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TicketBooking.Common.AppExceptions;
using TicketBooking.Service.Services.FlightService;
using TicketBooking.Model.DataModel;
using TicketBooking.Data.Repository;
using TicketBooking.Service.Models;

namespace TicketBookingAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class FlightControllers : ControllerBase
    {
        private IFlightService flightservice { get; }
        private IFlightValidation flightvalidate { get; }

        public FlightControllers(IFlightService flightservice,
            IFlightValidation flightvalidate)
        {
            this.flightservice = flightservice;
            this.flightvalidate = flightvalidate;
        }


        [HttpPost("add-flight")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddFlight(FlightRequestModel flightModel)
        {
            if (flightModel == null)
            {
                return BadRequest("No model to enter");
            }

            if (!flightvalidate.FlightDateValid(flightModel.DepartTime, flightModel.ArrivalTime))
            {
                return BadRequest("Wrong in time schedule");
            }

            if (!flightvalidate.AirportNameValid(flightModel.DepartAirportCode)
                && !flightvalidate.AirportNameValid(flightModel.ArrivalAirportCode))
            {
                return BadRequest("Wrong airport name");
            }

            flightModel.ArrivalAirportCode = flightvalidate.AirportNameProcess(flightModel.ArrivalAirportCode);
            flightModel.DepartAirportCode = flightvalidate.AirportNameProcess(flightModel.DepartAirportCode);
            
            return Accepted(await flightservice.InsertAsync(flightModel));
        }

        [HttpDelete]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RemoveFlight(Guid id)
        {
            await flightservice.RemoveAsync(id);
            return Accepted();
        }

        [HttpPut]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateFlight(FlightUpdateModel flightUpdateModel)
        {
            var result = await flightservice.UpdateFlightAsync(flightUpdateModel);
            if (result == 0)
            {
                return BadRequest("Cannot update flight. Flight already ended or not exist");
            }
            return Accepted();
        }

        [HttpGet]
        public async Task<IActionResult> GetFlight() => Ok(await flightservice.GetFlightAsync());

        [HttpGet("GetflightByID")]
        public async Task<ActionResult> GetFlightById(Guid id)
        {
            if ((await flightservice.GetFlightAsync(id)) == null)
            {
                return BadRequest("No flight matches with the ID");
            }
            
            return Ok(await flightservice.GetFlightAsync(id));
        }
        
        [HttpGet("GetflightByRequest")]
        public async Task<IActionResult> GetFlightByRequest([FromQuery] FlightRequest flightrequest)
        {
            if ((await flightservice.GetFlightAsync(flightrequest)) == null)
            {
                return BadRequest("No flight matches with the ID");
            }
            
            return Ok(await flightservice.GetFlightAsync(flightrequest));
        }
        
        [HttpGet("get-paging-flight-with-request")]
        public async Task<IActionResult> GetFlightByRequestPaging([FromQuery] FlightRequest request)
        {
            if ((await flightservice.GetFlightPagingAsync(request)) == null)
            {
                return BadRequest("No flight matches with the ID");
            }
            
            return Ok(await flightservice.GetFlightPagingAsync(request));
        }

        // This controller is for logic demo purpose.
        // It will be removed after being integrated into booking service
        [HttpPut("UpdateRemainSeat")]
        public async Task<IActionResult> UpdateRemainingSeat(Guid id, SeatClassType type, int number)
        {
            if((await flightservice.UpdateFlightSeat(id, type, number)))
            {
                return Ok("Update remaining seat successfully");
            }
            else
            {
                return BadRequest("Update remaining seat failed");
            }
        }

        [HttpPatch("UpdateFlightStatus")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateFlightStatus(Guid id)
        {
            return Ok(await flightservice.DeactiveFlightStatus(id));
        }
    }
}