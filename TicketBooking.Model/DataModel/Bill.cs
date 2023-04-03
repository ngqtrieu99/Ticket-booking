using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data.DataModel;

namespace TicketBooking.Model.DataModel
{
    [Table("Bill")]
    public class Bill
    {
        [Key]
        public Guid Id { get; set; }
        public long Amount { get; set; }
        public string OrderId { get; set; }
        public string Description { get; set; } 
        public string CreatedDate { get; set; }

        public long PaymentTranId { get; set; }
        public string BankCode { get; set; }
        public string PayStatus { get; set; }
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; }

    }
}
