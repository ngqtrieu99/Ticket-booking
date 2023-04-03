using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking.Data.DataModel
{
    [Table("FlightSchedule")]
    public class FlightSchedule
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid DepartureAirportId { get; set; }
        public Airport? AirportDepart { get; set; }
        [Required]
        public Guid ArrivalAirportId { get; set; }
        public Airport? AirportArrival { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        public ICollection<Flight> Flights { get; set; }
        public FlightSchedule()
        {
            Flights = new HashSet<Flight>();
        }
    }
}
