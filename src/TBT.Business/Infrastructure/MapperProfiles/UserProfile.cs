using AutoMapper;
using TBT.Business.Models.BusinessModels;
using TBT.DAL.Entities;

namespace TBT.Business.Infrastructure.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>().MaxDepth(1)
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company ?? new Company() { Id = src.CompanyId ?? 0 }))
                .ForMember(d => d.TimeEntries, opt => opt.Ignore());

            CreateMap<UserModel, User>()
                .ForMember(d => d.Projects, opt => opt.Ignore())
                .ForMember(d => d.TimeEntries, opt => opt.Ignore())
                .ForMember(d => d.Company, opt => opt.Ignore())
                .ForMember(d => d.CompanyId, opt => opt.MapFrom(src => src.Company.Id));


            CreateMap<Account, User>();
            CreateMap<User, Account>();
        }
    }
}
