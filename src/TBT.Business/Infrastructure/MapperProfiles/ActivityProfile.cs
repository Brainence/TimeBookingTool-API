using AutoMapper;
using TBT.Business.Models.BusinessModels;
using TBT.DAL.Entities;

namespace TBT.Business.Infrastructure.MapperProfiles
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<Activity, ActivityModel>()
                 .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project ?? new Project { Id = src.ProjectId }));

            CreateMap<ActivityModel, Activity>()
                .ForMember(dest => dest.ProjectId, dest => dest.MapFrom(src => src.Project != null && src.Project.Id > 0 ? src.Project.Id : default(int)))
                .ForMember(d => d.Project, opt => opt.Ignore());
        }
    }
}
