using AutoMapper;
using Core.DTOs;
using Domain.Entites;
using Domain.Repositories;
using Helpers.Login;
using Helpers.Responses;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Session> _sessionRepository;

        public UserService(IMapper mapper, IGenericRepository<User> userRepository, IGenericRepository<Session> sessionRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
        }

        public void Register(UserRegisterDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            userEntity.PasswordHash = LoginHelper.HashGen(user.Password);
            userEntity.userRoles = Domain.Enums.UserRole.User;
            userEntity.LastLogin = DateTime.Now;
            _userRepository.Add(userEntity);
        }

        public LogInResponse LogIn(UserLogInDTO user)
        {
            if (!new EmailAddressAttribute().IsValid(user.Email))
            {
                return new LogInResponse
                {
                    Status = false,
                    Message = "Invalid email format"
                };
            }

            var userEntity = _userRepository.GetAll().FirstOrDefault(x => x.Email == user.Email);
            if (userEntity == null)
            {
                return new LogInResponse
                {
                    Status = false,
                    Message = "User not found"
                };
            }

            var hashedPassword = LoginHelper.HashGen(user.Password);
            if (userEntity.PasswordHash != hashedPassword)
            {
                return new LogInResponse
                {
                    Status = false,
                    Message = "Invalid password"
                };
            }

            return new LogInResponse
            {
                Status = true,
                Message = userEntity.userRoles == Domain.Enums.UserRole.Admin ? "Admin" : "User"
            };
        }

        public HttpCookie Cookie(string email)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(email),
            };

            var validate = new EmailAddressAttribute();

            if (validate.IsValid(email))
            {
                var current = _sessionRepository.GetAll().FirstOrDefault(x => x.Email == email);

                if (current != null)
                {
                    current.CookieString = apiCookie.Value.Trim();
                    current.ExpireTime = DateTime.Now.AddMinutes(60);
                    _sessionRepository.Update(current);
                }
                else
                {
                    _sessionRepository.Add(new Session
                    {
                        Email = email,
                        CookieString = apiCookie.Value.Trim(),
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                }
            }
            return apiCookie;
        }

        public User GetUserByCookie(string cookie)
        {

            var test = _sessionRepository.GetAll();

            var session = _sessionRepository.GetAll().FirstOrDefault(x => x.CookieString == cookie);


            if (session == null) return null;

            var validate = new EmailAddressAttribute();
            if (validate.IsValid(session.Email))
            {
                var curentUser = _userRepository.GetAll().FirstOrDefault(u => u.Email == session.Email);
                if (curentUser != null)
                {
                    return curentUser;
                }
            }

            return null;
        }

        public void LogOut(string cookieValue)
        {
            var session = _sessionRepository.GetAll().FirstOrDefault(x => x.CookieString == cookieValue);
            if (session != null)
            {
                _sessionRepository.Delete(session.Id);
            }
        }
    }
}
