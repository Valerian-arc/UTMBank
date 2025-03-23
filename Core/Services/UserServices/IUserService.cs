using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.UserServices
{
    public interface IUserService
    {
        void Register(UserRegisterDTO user);
    }
}
