using AizenBankV1.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Helpers.RoleCheck
{
    public static class IsAdmin
    {
        public static bool IsUserAdmin()
        {
            var currentUser = HttpContext.Current.GetMySessionObject();

            if (currentUser.userRoles == Domain.Enums.UserRole.Admin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
