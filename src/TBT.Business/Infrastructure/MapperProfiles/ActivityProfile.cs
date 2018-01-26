using AutoMapper;
using TBT.Business.Models.BusinessModels;
using TBT.DAL.Entities;

namespace TBT.Business.Infrastructure.MapperProfiles
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<Activity, ActivityModel>();

            CreateMap<ActivityModel, Activity>()
                .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
                .ForMember(d => d.Project, opt => opt.Ignore());
        }
    }
}
