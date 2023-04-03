using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Data.DataModel
{
    [Table("ContactDetail")]
    public class ContactDetail
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string MiddleName { get; set; }

        [Required, MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string Email { get; set; }

        [Display(Name ="Phone Number")]
        [Required, MaxLength(11)]
        public string PhoneNumber { get; set; }

        //One to one relationship 
        public ICollection<Booking>? Bookings { get; set; }
        public ContactDetail()
        {
            Bookings = new HashSet<Booking>();
        }

    }
}
