using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Service.Models
{
    public class PaymentInformationModel
    {
        public string OrderType { get; set; }
        public Guid BookingId { get; set; }
    }
}
