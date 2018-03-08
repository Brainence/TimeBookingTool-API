using AutoMapper;
using TBT.Business.Models.BusinessModels;
using TBT.DAL.Entities;

namespace TBT.Business.Infrastructure.MapperProfiles
{
    public class ResetTicketProfile : Profile
    {
        public ResetTicketProfile()
        {
            CreateMap<ResetTicket, ResetTicketModel>();
            CreateMap<ResetTicketModel, ResetTicket>();
        }
    }
}
