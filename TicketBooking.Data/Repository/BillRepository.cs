using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data.DataModel;
using TicketBooking.Data.DbContext;
using TicketBooking.Data.Infrastructure;
using TicketBooking.Model.DataModel;

namespace TicketBooking.Data.Repository
{
    public interface IBillRepository : IRepository<Bill>
    {

    }
    public class BillRepository : GenericRepository<Bill>, IBillRepository
    {
        public BillRepository(TicketBookingDbContext context) : base(context)
        {
        }
    }
}
