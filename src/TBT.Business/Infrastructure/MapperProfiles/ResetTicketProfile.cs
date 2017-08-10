using AutoMapper;
using TBT.Business.Models.BusinessModels;
using TBT.DAL.Entities;

namespace TBT.Business.Infrastructure.MapperProfiles
{
    public class ResetTicketProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ResetTicket, ResetTicketModel>();
            CreateMap<ResetTicketModel, ResetTicket>();
        }
    }
}
