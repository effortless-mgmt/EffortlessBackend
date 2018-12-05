using System;
using AutoMapper;
using EffortlessApi.Core.Models;
using EffortlessLibrary.DTO;
using EffortlessLibrary.DTO.Address;
using EffortlessLibrary.DTO.Privilege;
using EffortlessLibrary.DTO.Role;

namespace EffortlessApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Address
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();

            //Agreement
            CreateMap<Agreement, AgreementDTO>();
            CreateMap<AgreementDTO, Agreement>();

            //Appointment
            CreateMap<Appointment, AppointmentDTO>();
            CreateMap<AppointmentDTO, Appointment>();
            CreateMap<AppointmentInDTO, Appointment>();
            CreateMap<Appointment, AppointmentInDTO>();
            CreateMap<Appointment, AppointmentSimpleDTO>();
            CreateMap<AppointmentSimpleDTO, Appointment>();
            CreateMap<Appointment, AppointmentStrippedDTO>();
            CreateMap<AppointmentStrippedDTO, Appointment>();

            //Company
            CreateMap<Company, CompanyDTO>();
            CreateMap<CompanyDTO, Company>();
            CreateMap<Company, CompanySimpleDTO>();
            CreateMap<CompanySimpleDTO, Company>();

            //Department
            CreateMap<Department, DepartmentDTO>();
            CreateMap<DepartmentDTO, Department>();
            CreateMap<Department, DepartmentStrippedDTO>();
            CreateMap<DepartmentStrippedDTO, Department>();

            //User
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<User, UserStrippedDTO>();
            CreateMap<UserStrippedDTO, User>();

            //WorkPeriod
            CreateMap<WorkPeriodInDTO, WorkPeriod>();
            CreateMap<WorkPeriod, WorkPeriodOutDTO>();
            CreateMap<WorkPeriodOutDTO, WorkPeriod>();

            //Role
            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();
            CreateMap<Role, RoleSimpleDTO>();
            CreateMap<RoleSimpleDTO, Role>();

            //Privilege
            CreateMap<Privilege, PrivilegeDTO>();
            CreateMap<PrivilegeDTO, Privilege>();
        }
    }
}
