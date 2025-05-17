using AizenBankV1.Web.Extensions;
using System.Web;

namespace Helpers.Checks
{
    public static class GetUserData
    {
        public static string GetUserName()
        {
            var currentUser = HttpContext.Current.GetMySessionObject();

            return currentUser.FirstName + " " + currentUser.LastName;
        }
    }
}
