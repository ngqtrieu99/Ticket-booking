using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking.Data.DataModel
{
    [Table("BookingSeat")]
    public class BookingSeat
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid SeatId { get; set; }
        public Seat? Seat { get; set; }
        [Required]
        public Guid BookingListId { get; set; }
        public BookingList? BookingList { get; set; }
    }
}
