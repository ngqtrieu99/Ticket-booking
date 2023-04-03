using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Service.Models
{
    public class PaymentResponseModel
    {
        public string OrderDescription { get; set; }
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public long PaymentId { get; set; }
        public long Amount { get; set; }
        public string PayDate { get; set; }
        public string BankCode { get; set; }
        public bool Success { get; set; }
        public string PaymentStatus { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }
}
