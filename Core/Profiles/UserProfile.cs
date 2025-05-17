using AutoMapper;
using Core.DTOs;
using Core.DTOs.Admin;
using Core.Models;
using Domain.Entites;
using Web.Models;

namespace Helpers.Profiles
{
    public class UserProfile : Profile 
    {
        public UserProfile()
        {
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            CreateMap<UserRegisterDTO, UserRegisterViewModel>().ReverseMap();
            CreateMap<User, UserLogInDTO>().ReverseMap();
            CreateMap<UserLogInDTO, UserLogInViewModel>().ReverseMap();
            CreateMap<UserRegisterDTO, User>().ReverseMap();
            CreateMap<UserEditDTO, User>()
                .ForMember(dest => dest.userRoles, opt => opt.MapFrom(src => src.Role));
            CreateMap<User, UserEditDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.userRoles));
            CreateMap<UserCreateDTO, User>().ReverseMap();
            CreateMap<UserCreateDTO, UserRegisterDTO>().ReverseMap();
        }
    }
}
