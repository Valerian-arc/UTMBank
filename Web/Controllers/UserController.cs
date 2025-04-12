using AutoMapper;
using Core.DTOs;
using Core.Services.UserServices;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService)
        {
            _userService = userService;
            _mapper = DependencyResolver.Current.GetService<IMapper>();
        }

        public ActionResult Index()
        {
            return View("Register");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegisterViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userDTO = _mapper.Map<UserRegisterDTO>(user);
            _userService.Register(userDTO);
            return RedirectToAction("Index", "Home");
        }
    }
}