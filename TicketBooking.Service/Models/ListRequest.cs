using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Service.Models
{
    public class ListRequest
    {
        public Guid BookingId   { get; set; }
        public List<BookingListViewModel>? BookingListViewModels { get; set; }
    }
}
