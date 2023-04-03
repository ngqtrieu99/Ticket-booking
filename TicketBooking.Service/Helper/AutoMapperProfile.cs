using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicketBooking.Data;
using TicketBooking.Data.DataModel;
using TicketBooking.Service.Models;

namespace TicketBooking.Service.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AircraftViewModel, Aircraft>().ReverseMap()
            .ForMember(dest => dest.Model, act => act.MapFrom(src => src.Model))
            .ForMember(dest => dest.Manufacture, act => act.MapFrom(src => src.Manufacture));

            CreateMap<Booking, BookingRequestModel>().ReverseMap();
            CreateMap<Booking, BookingViewModel>().ReverseMap();

            CreateMap<ContactViewModel, ContactDetail>().ReverseMap();
            CreateMap<BookingListViewModel, BookingList>().ReverseMap();
            CreateMap<PassengerViewModel, Passenger>().ReverseMap();

            CreateMap<Airport, AirportViewModel>().ReverseMap()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.Code, act => act.MapFrom(src => src.Code))
                .ForMember(dest => dest.Country, act => act.MapFrom(src => src.Country));

            CreateMap<Flight, FlightViewModel>()
                .ForMember(dest => dest.DepartTime, act => act.MapFrom(src => src.Schedule.DepartureTime.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dest => dest.ArrivalTime, act => act.MapFrom(src => src.Schedule.ArrivalTime.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(dest => dest.remainBusinessSeat, act => act.MapFrom(src => src.RemainBusinessSeat))
                .ForMember(dest => dest.remainEconomySeat, act => act.MapFrom(src => src.RemainEconomySeat))
                .ForMember(dest => dest.DepartAirport, act => act.MapFrom(src => src.Schedule.AirportDepart.Code))
                .ForMember(dest => dest.ArrivalAirport, act => act.MapFrom(src => src.Schedule.AirportArrival.Code))
                .ForPath(dest => dest.AircraftModel, act => act.MapFrom(src => src.Aircraft.Model))
                .ForPath(dest => dest.AircraftManufacture, act => act.MapFrom(src => src.Aircraft.Manufacture))
                .ForPath(dest => dest.AircraftManufacture, act => act.MapFrom(src => src.Aircraft.Manufacture))
                .ForMember(dest => dest.DepartTime, act => act.MapFrom(src => src.Schedule.DepartureTime))
                .ForMember(dest => dest.ArrivalTime, act => act.MapFrom(src => src.Schedule.ArrivalTime));

            CreateMap<FlightUpdateModel, Flight>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.AircraftId, act => act.MapFrom(src => src.AircraftId))
                .ForMember(dest => dest.DefaultBaggage, act => act.MapFrom(src => src.DefaultBaggage))
                .ForMember(dest => dest.EconomyPrice, act => act.MapFrom(src => src.EconomyPrice))
                .ForMember(dest => dest.BusinessPrice, act => act.MapFrom(src => src.BusinessPrice));
            
            CreateMap<FlightScheViewModel, FlightSchedule>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.DepartureAirportId, act => act.MapFrom(src => src.DepartureAirpotId))
            .ForMember(dest => dest.ArrivalAirportId, act => act.MapFrom(src => src.ArrivalAirportId));

            CreateMap<AirportViewModel, Airport>().ReverseMap();
            CreateMap<TicketViewModel, Ticket>().ReverseMap();
        }

    }
}