using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data.Repository;

namespace TicketBooking.Data.Infrastructure
{
    
    public interface IUnitOfWork : IDisposable
    {
        Task CompletedAsync();
    }
}
