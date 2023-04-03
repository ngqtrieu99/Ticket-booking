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
    internal sealed class OptionalAttribute : Attribute { }
    public class FlightUpdateModel
    {
        public Guid Id { set; get; }
        
        public Guid AircraftId { set; get; }
        
        public int DefaultBaggage { set; get; }

        public decimal BusinessPrice { set; get; }

        public decimal EconomyPrice { set; get; }
    } 
}

