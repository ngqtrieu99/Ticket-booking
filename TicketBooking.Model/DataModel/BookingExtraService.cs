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
    [Table("BookingService")]
    public class BookingExtraService
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid BookingListId { get; set; }
        public BookingList? BookingList { get; set; }
        [Required]
        public Guid ExtraServiceId { get; set; }
        public ExtraService? ExtraService { get; set; }
    }
}
