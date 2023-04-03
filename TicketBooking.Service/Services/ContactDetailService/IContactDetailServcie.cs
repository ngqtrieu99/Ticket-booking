using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Service.Models;

namespace TicketBooking.Service.Services.ContactDetailService
{
    public interface IContactDetailServcie
    {
        Task<Response> CreateContactInfo(ContactViewModel contact);
    }
}
