using AutoMapper;
using TBT.Business.Models.BusinessModels;
using TBT.DAL.Entities;

namespace TBT.Business.Infrastructure.MapperProfiles
{
    public class TimeEntryProfile : Profile
    {
        public TimeEntryProfile()
        {
            CreateMap<TimeEntry, TimeEntryModel>();

            CreateMap<TimeEntryModel, TimeEntry>()
                .ForMember(d => d.Activity, opt => opt.Ignore())
                .ForMember(d => d.User, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityId, opt => opt.MapFrom(src => src.Activity.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
        }
    }
}
