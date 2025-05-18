using AutoMapper;
using Core.DTOs;
using Core.Models;
using Core.Services.UserServices;
using System;
using System.Web.Mvc;
using System.Web.Security;
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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogInViewModel user)
        {
            Session.Clear();
            FormsAuthentication.SignOut();

            if (Response.Cookies["X-KEY"] != null)
            {
                Response.Cookies["X-KEY"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["X-KEY"].HttpOnly = true;
            }

            if (!ModelState.IsValid)
                return View(user);

            var userDTO = _mapper.Map<UserLogInDTO>(user);
            var response = _userService.LogIn(userDTO);

            if (!response.Status)
            {
                ModelState.AddModelError("", response.Message);
                return View(user);
            }

            var cookie = _userService.Cookie(user.Email);
            Response.Cookies.Add(cookie);
            Session["UserProfile"] = user.Email;

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            var cookie = Request.Cookies["X-KEY"];
            if (cookie != null)
            {
                _userService.LogOut(cookie.Value);

                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
            }

            Session.Abandon();
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "User");
        }
    }
}