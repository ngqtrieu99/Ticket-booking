using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Data.DataModel;
using TicketBooking.Data.Infrastructure;
using TicketBooking.Data.Repository;
using TicketBooking.Model.Models;
using TicketBooking.Service.Models;

namespace TicketBooking.Service.Services.ContactDetailService
{
    public class ContactDetailservice: IContactDetailServcie
    {
        private readonly IContactDetailRepository contactRepo;
        private IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ContactDetailservice(IContactDetailRepository contact, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.contactRepo = contact;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Response> CreateContactInfo(ContactViewModel contact)
        {
            var contactDetail = mapper.Map<ContactDetail>(contact);
            await contactRepo.Add(contactDetail);
            await unitOfWork.CompletedAsync();
            return new Response
            {
                Status = true,
                Message = "Create Contact Successfully",
            };
        }

    }
}
