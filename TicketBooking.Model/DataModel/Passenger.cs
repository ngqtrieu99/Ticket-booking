using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Data.DataModel
{
    [Table("Passenger")]
    public class Passenger
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string LastName { get; set; }

        [Required]
        public string DateOfBirth { get; set; }

        [Required, MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string Nation { get; set; }

        [Required, MaxLength(20)]
        public string IdentityCard { get; set; }

        [Required, MaxLength(50)]
        [Display(Name ="Provide Nation")]
        [Column(TypeName = "varchar")]
        public string ProvideNa { get; set; }

        [Display(Name = "Expire Nation")]
        public string ExpDate { get; set; }

        public Guid? BookingId { get; set; }

        public Booking? Booking { get; set; }

        public ICollection<Ticket>? Tickets { get; set; }
        public Passenger()
        {
            Tickets = new HashSet<Ticket>();
        }
    }
}
