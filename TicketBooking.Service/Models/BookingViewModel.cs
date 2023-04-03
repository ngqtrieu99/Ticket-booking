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
    public class BookingViewModel
    {
        public int NumberPeople { get; set; }
        public DateTime DateBooking { get; set; }
        public string Reference { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsPaid { get; set; }
        public string? Status { get; set; }
        public bool IsRoundFlight { get; set; }
        public List<TicketViewModel> Tickets { get; set; }
    }
}
