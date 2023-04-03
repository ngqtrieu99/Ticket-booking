using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data.DataModel;

namespace TicketBooking.Service.Models
{
    public class FlightScheUpdate
    {
        public DateTime DepartureTime { get; set; }
        
        public DateTime ArrivalTime { get; set; }
    }
}