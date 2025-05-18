using AutoMapper;
using Core.DTOs;
using Core.DTOs.Admin;
using Core.Services.UserServices;
using System.Web.Mvc;
using Web.Attributes;

namespace Web.Controllers
{
    [RequireLogin]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdminController(IUserService userService)
        {
            _userService = userService;
            _mapper = DependencyResolver.Current.GetService<IMapper>();
        }

        public ActionResult Index()
        {
            var users = _userService.GetUsers();
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userDTO = _mapper.Map<UserRegisterDTO>(model);
            _userService.Register(userDTO);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var model = _mapper.Map<UserEditDTO>(user);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserEditDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var existingUser = _userService.GetById(model.Id);
            if (existingUser == null)
            {
                return HttpNotFound();
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                model.Password = existingUser.PasswordHash;
            }

            _mapper.Map(model, existingUser);

            _userService.Update(existingUser);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            _userService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
