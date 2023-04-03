using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketBooking.Data.DataModel;

namespace TicketBooking.Service.Models
{
    public class AirportViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        public ICollection<FlightSchedule> DepartureAirports { get; set; }
        public ICollection<FlightSchedule> ArrivalAirports { get; set; }
    }
}
