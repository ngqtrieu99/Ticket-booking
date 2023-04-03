using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TicketBooking.Service.Models
{
    public class PassengerViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string DateOfBirth { get; set; }

        [Required]
        public string Nation { get; set; } = "Viet Nam";

        [Required]
        public string IdentityCard { get; set; } 

        [Required]
        public string ProvideNa { get; set; }
        public string ExpDate { get; set; }
    }
}
