using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data.DataModel;
using TicketBooking.Data.DbContext;
using TicketBooking.Data.Infrastructure;

namespace TicketBooking.Data.Repository
{
    public interface IContactDetailRepository : IRepository<ContactDetail>
    {
    }
    public class ContactDetailRepository : GenericRepository<ContactDetail>, IContactDetailRepository
    {
        public ContactDetailRepository(TicketBookingDbContext context) : base(context)
        {
        }
    }
}
