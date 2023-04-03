using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using TicketBooking.Data.DataModel;

namespace TicketBooking.Service.Models
{
    public class FlightViewModel
    {
        public Guid Id { get; set; }
        public string AircraftModel { set; get; }

        public string AircraftManufacture { get; set; }
        
        [Required]
        public bool IsFlightActive { set; get; }

        [Required]
        public int DefaultBaggage { set; get; }

        public decimal BusinessPrice { set; get; }

        public decimal EconomyPrice { set; get; }

        public string DepartAirport { set; get; }
        
        public string DepartTime { set; get; }
        
        public string ArrivalAirport { set; get; }
        
        public string ArrivalTime { set; get; }

        [Required]
        public int remainBusinessSeat { set; get; }
        
        [Required]
        public int remainEconomySeat { set; get; }
    }
}
