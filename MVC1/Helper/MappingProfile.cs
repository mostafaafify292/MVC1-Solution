using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MVC1.viewModels;
using MVC1__DAL_.Models;

namespace MVC1.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
            CreateMap<ApplicationUser , userViewModel>()
                .ForMember(u=>u.FName ,u2=>u2.MapFrom(u2=>u2.FirstName))
                .ForMember(u=>u.LName ,u2=>u2.MapFrom(u2=>u2.LastName)).ReverseMap();
            CreateMap<IdentityRole,RoleViewModel>().ForMember(v=>v.RoleName ,o=>o.MapFrom(i=>i.Name)).ReverseMap();

        }
    }
}
