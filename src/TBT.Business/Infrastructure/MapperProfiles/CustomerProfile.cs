using AutoMapper;
using System.Linq;
using TBT.Business.Models.BusinessModels;
using TBT.DAL.Entities;

namespace TBT.Business.Infrastructure.MapperProfiles
{
    public class CustomerProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Customer, CustomerModel>()
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Projects.Where(a => a.IsActive)));


            CreateMap<CustomerModel, Customer>()
                .ForMember(d => d.Projects, opt => opt.Ignore())
                .ForMember(d => d.Company, opt => opt.Ignore())
                .ForMember(d => d.CompanyId, opt => opt.MapFrom(src => src.Company.Id));
        }
    }
}
