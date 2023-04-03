using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking.Data.DataModel
{
    [Table("Seat")]
    public class Seat
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string SeatCode { get; set; }

        [Required]
        public Guid SeatClassId { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        [MaxLength(1)]
        public string CoordinateX { get; set; }

        [Required]
        public int CoordinateY { get; set; }

        public SeatClass? SeatClass { get; set; }

        [Required]
        public Guid AirCraftId { get; set; }

        //One to one relationship with ListSeat
        public Aircraft? Aircraft { get; set; }
        public BookingSeat? ListSeat { get; set; }
    }
}
