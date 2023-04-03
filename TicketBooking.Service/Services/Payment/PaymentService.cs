using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using TicketBooking.Common.AppExceptions;
using TicketBooking.Common.EnvironmentSetting;
using TicketBooking.Data.DataModel;
using TicketBooking.Data.Infrastructure;
using TicketBooking.Data.Repository;
using TicketBooking.Model.DataModel;
using TicketBooking.Service.Models;
using TicketBooking.Service.Services.FlightService;
using TicketBooking.Service.Services.SendMailService;

namespace TicketBooking.Service.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IBillRepository billRepository;
        private IUnitOfWork unitOfWork;
        private readonly VnpaySettings vnpaySettings;
        private readonly IConfiguration configuration;
        private readonly IBookingRepository bookingRepo;
        private readonly IContactDetailRepository contactRepo;
        private readonly ISendMailService sendMailService;
        private readonly ITicketRepository ticketRepo;
        private readonly IFlightService flightService;
        private readonly IBookingListRepository bookingListRepo;
        private readonly IFlightRepository flightRepo;

        public PaymentService(IOptions<VnpaySettings> options
            , IConfiguration _configuration
            , IBillRepository billRepository
            , IUnitOfWork unitOfWork
            , IBookingRepository bookingRepo
            , ISendMailService sendMailService
            , IContactDetailRepository contactDetail
            , ITicketRepository ticketRepo
            , IBookingListRepository bookingListRepo
            , IFlightService flightService
            , IFlightRepository flightRepo)
        {
            this.vnpaySettings = options.Value;
            this.configuration = _configuration;
            this.billRepository = billRepository;
            this.unitOfWork = unitOfWork;
            this.bookingRepo = bookingRepo;
            this.sendMailService = sendMailService;
            contactRepo = contactDetail;
            this.ticketRepo = ticketRepo;
            this.flightService = flightService;
            this.bookingListRepo = bookingListRepo;
            this.flightRepo = flightRepo;
        }

        public async Task<string> CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            var booking = await bookingRepo.GetById(model.BookingId);
            if (booking == null)
            {
                return "Invalid booking";
            }
            else if (booking.IsPaid == true)
            {
                return "This booking is paid";
            }
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = configuration["PaymentCallBack:ReturnUrl"];

            pay.AddRequestData("vnp_Version", vnpaySettings.Version);
            pay.AddRequestData("vnp_Command", vnpaySettings.Command);
            pay.AddRequestData("vnp_TmnCode", vnpaySettings.TmnCode);
            pay.AddRequestData("vnp_Amount", ((int)booking.TotalPrice * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", vnpaySettings.CurrCode);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", vnpaySettings.Locale);
            pay.AddRequestData("vnp_OrderInfo", $"{model.BookingId}");
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(vnpaySettings.BaseUrl, vnpaySettings.HashSecret);

            return paymentUrl;
        }

        public async Task<PaymentResponseModel> PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = await pay.GetFullResponseData(collections, configuration["Vnpay:HashSecret"]);
            await SendTicket(response);
            await SavePayment(response);
            await ChangeNumSeat(response);
            return response;
        }

        public async Task SendTicket(PaymentResponseModel response)
        {
            var booking = await bookingRepo.GetById(new Guid(response.OrderDescription));
            var tickets = ticketRepo.Find(ticket => ticket.BookingId == booking.Id).ToList();
            var bookingList = bookingListRepo.Find(x => x.BookingId == booking.Id);
            var contact = await contactRepo.GetById((Guid)booking.ContactId);
            var combined = string.Empty;
            //SeatClassType type;
            //Enum.TryParse<SeatClassType>(tickets[0].SeatClass, out type);
            if (response.PaymentStatus != "Success")
            {
                return;
            }

            foreach (var ticket in tickets)
            {

                string caText = $"Passenger Name:{ticket.PassengerName}, Location From:{ticket.LocationFrom}, Location To:{ticket.LocationTo}, Seat Class:{ticket.SeatClass}, Departure Time:{ticket.DepartureTime}, AirlineName{ticket.AirlineName}, AircraftModel{ticket.AircraftModel}, BookingCode{ticket.BookingCode}";
                combined += caText;
            }

            var mailRequest = new MailRequest()
            {
                ToEmail = contact.Email,
                Subject = "You have sucessfully paid",
                Body = combined
            };
            await sendMailService.SendEmailAsync(mailRequest);
        }

        public async Task SavePayment(PaymentResponseModel response)
        {
            var booking = await bookingRepo.GetById(new Guid(response.OrderDescription));

            var bill = new Bill()
            {
                Amount = response.Amount,
                OrderId = response.OrderId,
                Description = response.OrderDescription,
                CreatedDate = response.PayDate,
                PaymentTranId = response.PaymentId,
                BankCode = response.BankCode,
                PayStatus = response.Success.ToString(),
                BookingId = booking.Id,
            };
            await billRepository.Add(bill);
            if (response.PaymentStatus == "Success")
            {
                booking.IsPaid = true;
            }
            booking.Bills.Add(bill);
            bookingRepo.Update(booking);
            await unitOfWork.CompletedAsync();
        }
        public async Task ChangeNumSeat(PaymentResponseModel response)
        {
            var booking = await bookingRepo.GetById(new Guid(response.OrderDescription));
            var tickets = ticketRepo.Find(ticket => ticket.BookingId == booking.Id).ToList();
            var bookingList = bookingListRepo.Find(x => x.BookingId == booking.Id).ToList();
            var flights = new List<Flight>();
            SeatClassType type;
            Enum.TryParse<SeatClassType>(tickets[0].SeatClass, out type);
            foreach(var item in bookingList)
            {
                var flight = await flightRepo.GetById((Guid)item.FlightId);
                flights.Add(flight);
            }
            foreach (var item in flights) 
            {
                await flightService.UpdateFlightSeat(item.Id, type, booking.NumberPeople);
            }
        }
    }
}
