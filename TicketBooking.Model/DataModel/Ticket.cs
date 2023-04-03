using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Data.DataModel
{
    [Table("Ticket")]
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Display(Name ="Passenger Name")]
        public string PassengerName { get; set; }

        [MaxLength(50)]
        [Display(Name ="Location From")]
        public string LocationFrom { get; set; }
        [MaxLength(50)]
        [Display(Name = "Location To")]
        public string LocationTo { get; set; }

        [MaxLength(20)]
        public string SeatClass { get; set; }
  
        public DateTime DepartureTime { get; set; }

        [MaxLength(50)]
        public string? AirlineName { get; set; }

        [MaxLength(6)]
        public string? AircraftModel { get; set; }
        public string BookingCode { get; set; }
        public Guid? BookingId { get; set; }
        public Booking? Booking { get; set; }
        public Guid? PassengerId { get; set; }
        public Passenger? Passenger { get; set; }
    }
}
