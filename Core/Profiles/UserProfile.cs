using AutoMapper;
using Core.DTOs;
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
        }
    }
}
