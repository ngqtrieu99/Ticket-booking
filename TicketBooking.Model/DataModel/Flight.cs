using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking.Data.DataModel
{
    [Table("Flight")]
    public class Flight
    {
        [Key]
        public Guid Id { set; get; }

        [Required]
        public Guid AircraftId { set; get; }
        public Aircraft? Aircraft { set; get;}
        [Required]
        public int TotalSeat { set; get; }

        [Required]
        public int RemainingSeat { set; get; }
        
        [Required]
        public int RemainEconomySeat { set; get; }
        
        [Required]
        public int RemainBusinessSeat { set; get; }

        [Required]
        public bool IsFlightActive { set; get; }

        [Required]
        public int DefaultBaggage { set; get; }
        public decimal BusinessPrice { set; get; }
        public decimal EconomyPrice { set; get; }

        [Required]
        public Guid ScheduleId { set; get; }
        public FlightSchedule? Schedule { set; get; }


        public ICollection<BookingList> BookingLists { get; set; }

        public Flight()
        {
            BookingLists= new HashSet<BookingList>();
        }
    }
}