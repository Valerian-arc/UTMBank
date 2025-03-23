using AutoMapper;
using Core.DTOs;
using Domain.Data.Context;
using Domain.Entites;
using Domain.Repositories;
using System;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Web.Mvc;

namespace Core.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> _repository;

        public UserService(BankDbContext context, IGenericRepository<User> repository)
        {
            _mapper = DependencyResolver.Current.GetService<IMapper>();
            _repository = repository;
        }

        public void Register(UserRegisterDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            userEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            userEntity.userRoles = Domain.Enums.UserRole.User;
            userEntity.LastLogin = DateTime.Now;
            _repository.Add(userEntity);
        }
    }
}
