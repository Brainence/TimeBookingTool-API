using AutoMapper;
using TBT.Business.Models.BusinessModels;
using TBT.DAL.Entities;

namespace TBT.Business.Infrastructure.MapperProfiles
{
    public class ActivityProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Activity, ActivityModel>();

            CreateMap<ActivityModel, Activity>()
                .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    if (src.Project != null)
                        dest.ProjectId = src.Project.Id;
                })
                .ForMember(d => d.Project, opt => opt.Ignore());
        }
    }
}
