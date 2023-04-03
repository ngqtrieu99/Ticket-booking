using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace TicketBooking.Data.DataModel
{
    [Table("Aircraft")]
    public class Aircraft
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(6)]
        public string Model { get; set; } = null!;

        [Required]
        [MaxLength(6)]
        public string Manufacture { get; set; }

        [Required]
        public int NumRowSeat { get; set; }

        [Required]
        public int NumColumnSeat { get; set; }

        [Required]
        public int NumRowBusiness { get; set; }

        [Required]
        public int NumRowEconomy { get; set; }

        public ICollection<Seat>? Seats { get; set; }
        public ICollection<Flight>? Flights { get; set; }
        public Aircraft()
        {
            Seats = new HashSet<Seat>();
            Flights = new HashSet<Flight>();
        }
    }
}
