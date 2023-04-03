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
    public interface IRefreshTokenRepository
    {
        RefreshToken FindRefreshToken(string token);
        Task<bool> AddToken(RefreshToken token);
        void UpdateToken(RefreshToken token);
    }
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(TicketBookingDbContext context) : base(context)
        {
        }

        public async Task<bool> AddToken(RefreshToken token)
        {
            return await Add(token);
        }

        public RefreshToken FindRefreshToken(string token)
        {
            return Find(element => element.Token.Equals(token)).FirstOrDefault();
        }

        public void UpdateToken(RefreshToken token)
        {
            Update(token);
        }
    }
}