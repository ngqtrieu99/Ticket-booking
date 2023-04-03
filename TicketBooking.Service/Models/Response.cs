using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Service.Models

{
    public class Response
    {
        public bool? Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public IEnumerable<object>? List { get; set; } 
    }
        
}
