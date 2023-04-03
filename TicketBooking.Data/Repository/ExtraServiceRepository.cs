using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data.DbContext;
using TicketBooking.Data.Infrastructure;
using TicketBooking.Model.DataModel;

namespace TicketBooking.Data.Repository
{
    public interface IExtraServiceRepository : IRepository<ExtraService>
    {
    }
    public class ExtraServiceRepository : GenericRepository<ExtraService>, IExtraServiceRepository
    {
        public ExtraServiceRepository(TicketBookingDbContext context) : base(context)
        {

        }
        
    }
}
