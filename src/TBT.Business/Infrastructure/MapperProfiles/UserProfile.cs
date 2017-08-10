using AutoMapper;
using System.Linq;
using TBT.Business.Models.BusinessModels;
using TBT.DAL.Entities;

namespace TBT.Business.Infrastructure.MapperProfiles
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Projects.Where(a => a.IsActive)))
                .ForMember(d => d.TimeEntries, opt => opt.Ignore());

            CreateMap<UserModel, User>()
                .ForMember(d => d.Projects, opt => opt.Ignore())
                .ForMember(d => d.TimeEntries, opt => opt.Ignore());


            CreateMap<Account, User>();
            CreateMap<User, Account>();

        }
    }
}
