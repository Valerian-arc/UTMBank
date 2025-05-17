using Core.DTOs;
using Core.Models;
using Domain.Entites;
using Helpers.Responses;
using System.Collections.Generic;
using System.Web;

namespace Core.Services.UserServices
{
    public interface IUserService
    {
        void Register(UserRegisterDTO user);
        LogInResponse LogIn(UserLogInDTO user);
        HttpCookie Cookie(string email);
        User GetUserByCookie(string cookie);
        void LogOut(string cookieValue);
        List<User> GetUsers();
        User GetById(int id);
        void Update(User user);
        void Delete(int id);
    }
}
