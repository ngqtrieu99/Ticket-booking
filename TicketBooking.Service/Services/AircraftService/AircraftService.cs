using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBooking.Data.DataModel;
using TicketBooking.Data.Infrastructure;
using AutoMapper;
using TicketBooking.Data.Repository;
using TicketBooking.Data.DataModel;
using TicketBooking.Service.Models;

namespace TicketBooking.Service.Services.AircraftService
{
    public class AircraftService : IAircraftSerivce
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAircraftRepository aircraftRepo;
        public AircraftService(IUnitOfWork unitOfWork, IMapper mapper, IAircraftRepository aircraft)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            aircraftRepo = aircraft;
        }

        public async Task<IEnumerable<AircraftViewModel>> GetAircraftAsync()
        {
            var aircraft = await aircraftRepo.GetAll();
            if (aircraft == null)
            {
                throw new Exception("No data to display");
            }

            else
            {
                return mapper.Map<IEnumerable<AircraftViewModel>>(aircraft);
            }
        }

        public async Task<AircraftViewModel> GetAircraftAsync(Guid id)
        {
            var aircraft = await aircraftRepo.GetById(id);
            return aircraft == null ? throw new Exception("ID cannot be found") : mapper.Map<AircraftViewModel>(aircraft);
        }

        public async Task UpdateAircraftAsync(AircraftViewModel aircraftDto)
        {
            var aircraft = mapper.Map<Aircraft>(aircraftDto);
            aircraftRepo.Update(aircraft);
            await unitOfWork.CompletedAsync();
        }

        public async Task<string> InsertAsync(AircraftViewModel aircraftDto)
        {
            var aircraft = mapper.Map<Aircraft>(aircraftDto);
            await aircraftRepo.Add(aircraft);
            await unitOfWork.CompletedAsync();
            return "Add succesfully";
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var aircraft = aircraftRepo.Find(c => c.Id == id).FirstOrDefault();
            if (aircraft == null)
            {
                throw new Exception("ID is not found");
            }

            else
            {
                await aircraftRepo.Remove(aircraft.Id);
                await unitOfWork.CompletedAsync();
                return true;
            }
        }
    }
}