using AizenBankV1.Web.Extensions;
using AutoMapper;
using Core.Services.UserServices;
using Domain.Data.Context;
using Domain.Entites;
using Domain.Repositories;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Attributes
{
    public class RequireLoginAttribute : ActionFilterAttribute
    {
        private readonly IUserService _userService;

        public RequireLoginAttribute()
        {
            var context = new BankDbContext();
            var userRepository = new GenericRepository<User>(context);
            var sessionRepository = new GenericRepository<Session>(context);
            var mapper = DependencyResolver.Current.GetService<IMapper>();

            _userService = new UserService(mapper, userRepository, sessionRepository);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _userService.GetUserByCookie(apiCookie.Value);
                if (profile == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Login" }));
                }
                else
                {
                    HttpContext.Current.SetMySessionObject(profile);
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Login" }));
            }
        }
    }
}