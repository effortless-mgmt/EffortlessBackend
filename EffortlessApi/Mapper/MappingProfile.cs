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
            CreateMap<Appointment, AppointmentStrippedDTO>();
            CreateMap<AppointmentStrippedDTO, Appointment>();
            CreateMap<Appointment, AppointmentWpDTO>();
            CreateMap<AppointmentWpDTO, Appointment>();

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

            //Privilege
            CreateMap<Privilege, PrivilegeDTO>();
            CreateMap<PrivilegeDTO, Privilege>();

            //Role
            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();
            CreateMap<Role, RoleSimpleDTO>();
            CreateMap<RoleSimpleDTO, Role>();

            //RolePrivilege
            CreateMap<RolePrivilege, RolePrivilegeDTO>();
            CreateMap<RolePrivilegeDTO, RolePrivilege>();

            //User
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<User, UserSimpleDTO>();
            CreateMap<UserSimpleDTO, User>();
            CreateMap<User, UserStrippedDTO>();
            CreateMap<UserStrippedDTO, User>();

            //UserRole
            CreateMap<UserRole, UserRoleDTO>();
            CreateMap<UserRoleDTO, UserRole>();

            //UserWorkPeriod
            CreateMap<UserWorkPeriod, UserWorkPeriodDTO>();
            CreateMap<UserWorkPeriodDTO, UserWorkPeriod>();

            //WorkPeriod
            CreateMap<WorkPeriodInDTO, WorkPeriod>();
            CreateMap<WorkPeriod, WorkPeriodOutDTO>();
            CreateMap<WorkPeriodOutDTO, WorkPeriod>();
            CreateMap<WorkPeriod, WorkPeriodUserAppointmentDTO>();
            CreateMap<WorkPeriodUserAppointmentDTO, WorkPeriod>();
        }
    }
}
