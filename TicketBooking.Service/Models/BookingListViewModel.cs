using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data.DataModel;

namespace TicketBooking.Service.Models
{
    public class BookingListViewModel
    {
        public Guid? BookingId { get; set; }
        public Guid? FlightId { get; set; }
        public int NumberSeat { get; set; }
        public decimal FlightPrice { get; set; }
    }
}
