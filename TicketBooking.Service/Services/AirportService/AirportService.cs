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

namespace TicketBooking.Service.Services.AirportService
{
    public class AirportService : IAirportService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAirportRepository airportRepo;
        public AirportService(IUnitOfWork unitOfWork, IMapper mapper, IAirportRepository airportRepo)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.airportRepo = airportRepo;
        }

        public async Task<IEnumerable<AirportViewModel>> GetAirportAsync()
        {
            var airport = await airportRepo.GetAll();
            if (airport == null)
            {
                throw new Exception("No data to display");
            }

            else
            {
                return mapper.Map<IEnumerable<AirportViewModel>>(airport);
            }
        }

        public async Task<AirportViewModel> GetAirportAsync(Guid id)
        {
            var airport = await airportRepo.GetById(id);
            return airport == null ? throw new Exception("ID cannot be found") : mapper.Map<AirportViewModel>(airport);
        }

        public async Task<AirportViewModel> GetAirportAsync(string code)
        {
            var airport = await airportRepo.GetByAirportCode(code);
            return airport == null ? throw new Exception("ID cannot be found") : mapper.Map<AirportViewModel>(airport);
        }

        public async Task<string> UpdateAirportAsync(AirportViewModel airportViewModel)
        {
            var airport = mapper.Map<Airport>(airportViewModel);
            airportRepo.Update(airport);
            await unitOfWork.CompletedAsync();
            return "Update success";
        }

        public async Task<string> InsertAsync(AirportViewModel airportViewModel)
        {
            var airport = mapper.Map<Airport>(airportViewModel);
            await airportRepo.Add(airport);
            await unitOfWork.CompletedAsync();
            return "Insert success";
        }

        public async Task<string> RemoveAsync(Guid id)
        {
            var airport = airportRepo.Find(c => c.Id == id).FirstOrDefault();
            if (airport == null)
            {
                throw new Exception("ID is not found");
            }

            else
            {
                await airportRepo.Remove(airport.Id);
                await unitOfWork.CompletedAsync();
                return "Remove success";
            }
        }
    }
}
