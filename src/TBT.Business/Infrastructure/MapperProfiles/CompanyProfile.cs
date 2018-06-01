﻿using AutoMapper;
using System.Linq;
using TBT.Business.Models.BusinessModels;
using TBT.DAL.Entities;

namespace TBT.Business.Infrastructure.MapperProfiles
{
    public class CompanyProfile: Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyModel>()
                .ForMember(dest => dest.Customers, opt => opt.MapFrom(src => src.Customers.Where(x => x.IsActive)))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.Where(x => x.IsActive)));

            CreateMap<CompanyModel, Company>()
                .ForMember(dest => dest.Customers, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore());
        }
    }
}