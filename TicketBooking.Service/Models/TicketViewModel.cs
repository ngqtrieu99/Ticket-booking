using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TicketBooking.Service.Models
{
    public class TicketViewModel
    {
        public string PassengerName { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string SeatClass { get; set; }

        public DateTime DepartureTime { get; set; }
        public string? AirlineName { get; set; }

        public string? AircraftModel { get; set; }
        public string BookingCode { get; set; }
    }
}
