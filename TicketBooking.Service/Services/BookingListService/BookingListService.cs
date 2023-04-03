using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data.DataModel;
using TicketBooking.Data.Infrastructure;
using TicketBooking.Data.Repository;
using TicketBooking.Service.Models;

namespace TicketBooking.Service.Services.BookingListService
{
    public class BookingListService : IBookingListService
    {
        private readonly IBookingListRepository bookingListRepo;
        private IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public BookingListService(IBookingListRepository bookingListRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.bookingListRepo = bookingListRepo;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> AddBookingList(BookingListViewModel bookingList)
        {
            var contactDetail = mapper.Map<BookingList>(bookingList);
            await bookingListRepo.Add(contactDetail);
            await unitOfWork.CompletedAsync();
            return true;
        }
    }
}
