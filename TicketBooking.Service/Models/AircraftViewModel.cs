using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace TicketBooking.Service.Models

{
    public class AircraftViewModel
    {
        [Required]
        [MaxLength(6)]
        public string Model { get; set; }

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
    }
}