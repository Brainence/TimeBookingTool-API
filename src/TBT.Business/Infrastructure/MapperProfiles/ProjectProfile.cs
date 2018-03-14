using AutoMapper;
using System.Linq;
using TBT.Business.Models.BusinessModels;
using TBT.DAL.Entities;

namespace TBT.Business.Infrastructure.MapperProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectModel>()
                .ForMember(dest => dest.Activities, opt => opt.MapFrom(src => src.Activities.Where(a => a.IsActive)));

            CreateMap<ProjectModel, Project>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
                .ForMember(d => d.Customer, opt => opt.Ignore())
                .ForMember(d => d.Activities, opt => opt.Ignore())
                .ForMember(d => d.Users, opt => opt.Ignore());
        }
    }
}
