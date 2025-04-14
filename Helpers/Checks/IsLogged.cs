using AizenBankV1.Web.Extensions;
using System.Web;

namespace Helpers.Checks
{
    public static class IsLogged
    {
        public static bool IsUserLogged()
        {
            var currentUser = HttpContext.Current.GetMySessionObject();

            return currentUser != null;
        }
    }
}
