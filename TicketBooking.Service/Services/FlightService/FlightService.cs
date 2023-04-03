using TicketBooking.Common.AppExceptions;
using TicketBooking.Data.DataModel;
using TicketBooking.Data.Infrastructure;
using AutoMapper;
using NLog;
using TicketBooking.Data.Repository;
using TicketBooking.Service.Services.AirportService;
using TicketBooking.Service.Services.FlightScheService;
using TicketBooking.Service.Models;
using TicketBooking.Model.DataModel;

namespace TicketBooking.Service.Services.FlightService
{
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IFlightRepository flightRepo;
        private readonly IAircraftRepository aircraftRepo;
        private readonly IAirportService airportService;
        private readonly IFlightScheServices flightScheServices;
        private readonly IFlightScheduleRepository flightScheRepo;
        private readonly IAirportRepository airportRepo;

        public FlightService(IUnitOfWork unitOfWork, IMapper mapper,
                             IFlightRepository flightRepo, IAircraftRepository aircraftRepo,
                             IAirportService airportService, IFlightScheServices flightScheServices,
                             IFlightScheduleRepository flightScheRepo, IAirportRepository airportRepo)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.flightRepo = flightRepo;
            this.aircraftRepo = aircraftRepo;
            this.airportService = airportService;
            this.flightScheServices = flightScheServices;
            this.flightScheRepo = flightScheRepo;
            this.airportRepo = airportRepo;
        }


        public async Task<IEnumerable<FlightViewModel>> GetFlightAsync()
        {
            var flights = await flightRepo.GetAllFlight();
            var flightSche = new List<FlightSchedule>();
            var airports = new List<Airport>();
            
            foreach(var flight in flights)
            {
                await flightScheRepo.GetById(flight.ScheduleId);
                await aircraftRepo.GetById(flight.AircraftId);
                await airportRepo.GetById(flight.Schedule.DepartureAirportId);
                await airportRepo.GetById(flight.Schedule.ArrivalAirportId);
            }
            
            return flights.Any() 
                    ? mapper.Map<IEnumerable<FlightViewModel>>(flights) 
                    : throw new Exception("No flight available now");
        }

        public async Task<FlightViewModel> GetFlightAsync(Guid id)
        {
            var flight = await flightRepo.GetFlightById(id);
            var flightSche = new List<FlightSchedule>();
            var airports = new List<Airport>();
            
            await flightScheRepo.GetById(flight.ScheduleId);
            await aircraftRepo.GetById(flight.AircraftId);
            await airportRepo.GetById(flight.Schedule.DepartureAirportId);
            await airportRepo.GetById(flight.Schedule.ArrivalAirportId);
            
            return flight == null ? throw new Exception("ID does not exist") : mapper.Map<FlightViewModel>(flight);
        }

        public async Task<IEnumerable<FlightViewModel>> GetFlightAsync(FlightRequest flightModel)
        {
            var flights = await flightRepo.GetFlightByRequest(flightModel);
            var flightSche = new List<FlightSchedule>();
            var airports = new List<Airport>();
            
            foreach(var flight in flights)
            {
                await flightScheRepo.GetById(flight.ScheduleId);
                await aircraftRepo.GetById(flight.AircraftId);
                await airportRepo.GetById(flight.Schedule.DepartureAirportId);
                await airportRepo.GetById(flight.Schedule.ArrivalAirportId);
            }
            
            return flights.Any()
                ? mapper.Map<IEnumerable<FlightViewModel>>(flights) :
                throw new Exception("None flight match request");
        }
        
        public async Task<IEnumerable<FlightViewModel>> GetFlightPagingAsync(FlightRequest request)
        {
            var flights = await flightRepo.GetFlightPagingByRequest(request);
            var flightSche = new List<FlightSchedule>();
            var airports = new List<Airport>();
            
            foreach(var flight in flights)
            {
                await flightScheRepo.GetById(flight.ScheduleId);
                await aircraftRepo.GetById(flight.AircraftId);
                await airportRepo.GetById(flight.Schedule.DepartureAirportId);
                await airportRepo.GetById(flight.Schedule.ArrivalAirportId);
            }
            return flights.Any() ? mapper.Map<IEnumerable<FlightViewModel>>(flights) : throw new Exception("None flight match request");
        }

        public async Task<int> UpdateFlightAsync(FlightUpdateModel flightUpdateModel)
        {
            var flight = await flightRepo.GetById(flightUpdateModel.Id);


            if (flight.IsFlightActive == false)
            {
                return 0;
            }

            // Update flight information
            flight.Id = flightUpdateModel.Id;
            flight.AircraftId = (flightUpdateModel.AircraftId != null) ? flightUpdateModel.AircraftId : flight.AircraftId;
            flight.DefaultBaggage = (flightUpdateModel.DefaultBaggage != null) ? flightUpdateModel.DefaultBaggage : flight.DefaultBaggage;
            flight.BusinessPrice = (flightUpdateModel.BusinessPrice != null) ? flightUpdateModel.BusinessPrice : flight.BusinessPrice;
            flight.EconomyPrice = (flightUpdateModel.EconomyPrice != null) ? flightUpdateModel.EconomyPrice : flight.EconomyPrice;


            flightRepo.Update(flight);

            await unitOfWork.CompletedAsync();
            return 1;
        }

        public async Task<Guid> InsertAsync(FlightRequestModel flightRequestModel)
        {
            var tempAircarftModel = await aircraftRepo.GetById(flightRequestModel.AircraftId);
            if (tempAircarftModel == null)
            {
                throw new Exception("The Aircraft ID" + flightRequestModel.AircraftId + "does not exist");
            }

            var flight = new Flight()
            {
                Id = Guid.NewGuid(),
                AircraftId = flightRequestModel.AircraftId,
                TotalSeat = tempAircarftModel.NumColumnSeat * tempAircarftModel.NumRowSeat,
                RemainingSeat = tempAircarftModel.NumColumnSeat * tempAircarftModel.NumRowSeat,
                RemainEconomySeat = tempAircarftModel.NumColumnSeat * tempAircarftModel.NumRowEconomy,
                RemainBusinessSeat = tempAircarftModel.NumColumnSeat * tempAircarftModel.NumRowBusiness,
                ScheduleId = Guid.NewGuid(),
                DefaultBaggage = flightRequestModel.DefaultBaggage,
                IsFlightActive = true, // true as default - flight is available, customer can book it
                BusinessPrice = flightRequestModel.BusinessPrice,
                EconomyPrice = flightRequestModel.EconomyPrice
            };

            if (flightRequestModel.DepartTime == null || flightRequestModel.ArrivalTime == null)
            {
                throw new Exception("Depart and Arrival Time cannot be null");
            }
            else
            {
                var departCode = await airportService.GetAirportAsync(flightRequestModel.DepartAirportCode);
                var arrivalCode = await airportService.GetAirportAsync(flightRequestModel.ArrivalAirportCode);

                var flightScheRequestModel = new FlightScheViewModel()
                {
                    Id = flight.ScheduleId,
                    DepartureAirpotId = departCode.Id,
                    ArrivalAirportId = arrivalCode.Id,
                    DepartureAirportCode = departCode.Code,
                    ArrivalAirportCode = arrivalCode.Code,
                    DepartureTime = flightRequestModel.DepartTime,
                    ArrivalTime = flightRequestModel.ArrivalTime
                };

                await flightScheServices.InsertAsync(flightScheRequestModel);
            }

            await flightRepo.Add(flight);
            await unitOfWork.CompletedAsync();
            return flight.Id;
        }

        public async Task<string> RemoveAsync(Guid id)
        {
            var flight = flightRepo.Find(c => c.Id == id).FirstOrDefault();
            if (flight == null)
            {
                throw new Exception("ID is not found");
            }

            else
            {
                await flightRepo.Remove(flight.Id);
                await unitOfWork.CompletedAsync();
                return "Remove sucess";
            }
        }

        public async Task<bool> UpdateFlightSeat(Guid flightId, SeatClassType type, int number)
        {
            var flight = await flightRepo.GetById(flightId);
            if (type == SeatClassType.Business)
            {
                flight.RemainBusinessSeat = flight.RemainBusinessSeat - number;

                if (flight.RemainBusinessSeat < 0)
                {
                    return false;
                }
            }

            if (type == SeatClassType.Economy)
            {
                flight.RemainEconomySeat = flight.RemainEconomySeat - number;

                if (flight.RemainEconomySeat < 0)
                {
                    return false;
                }
            }

            flight.RemainingSeat = flight.RemainingSeat - number;

            if (flight.RemainingSeat < 0)
            {
                return false;
            }


            var modifieldflight = flight;

            flightRepo.Update(modifieldflight);

            await unitOfWork.CompletedAsync();
            return true;
        }

        public async Task<string> DeactiveFlightStatus(Guid flightId, bool status = false)
        {
            var flight = await flightRepo.GetById(flightId);
            flight.IsFlightActive = status;
            flightRepo.Update(flight);
            await unitOfWork.CompletedAsync();
            return "Flight has the ID number " + flight.Id + " has been deactived succesfully";
        }
    }
}
