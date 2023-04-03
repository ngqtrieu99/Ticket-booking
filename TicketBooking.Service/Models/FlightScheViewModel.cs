using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using TicketBooking.Data.DataModel;
using TicketBooking.Data;

namespace TicketBooking.Service.Models
{
    public class FlightScheViewModel
    {
        [Required]
        public Guid Id { get; set; }

        public Guid? DepartureAirpotId { get; set; }


        public Guid? ArrivalAirportId { get; set; }

        public string DepartureAirportCode { get; set; }

        public Airport AirportDepart { get; set; }

        public string ArrivalAirportCode { get; set; }

        public Airport AirportArrival { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }
    }
}
