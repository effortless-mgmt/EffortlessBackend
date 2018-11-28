using System;
using AutoMapper;
using EffortlessApi.Core.Models;
using EffortlessLibrary.DTO;

namespace EffortlessApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();
            CreateMap<Company, CompanyDTO>();
            CreateMap<CompanyDTO, Company>();

        }
    }
}
