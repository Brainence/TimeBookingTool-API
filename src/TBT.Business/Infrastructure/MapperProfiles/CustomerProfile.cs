﻿using AutoMapper;
using System.Linq;
using TBT.Business.Models.BusinessModels;
using TBT.DAL.Entities;

namespace TBT.Business.Infrastructure.MapperProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerModel>().MaxDepth(1)
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company ?? new Company() { Id = src.CompanyId.Value }))
                .PreserveReferences();

            CreateMap<CustomerModel, Customer>()
                .ForMember(d => d.Projects, opt => opt.Ignore())
                .ForMember(d => d.Company, opt => opt.Ignore())
                .ForMember(d => d.CompanyId, opt => opt.MapFrom(src => src.Company.Id));
        }
    }
}
