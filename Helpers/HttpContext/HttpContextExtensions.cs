using Domain.Entites;
using System;
using System.Web;

namespace AizenBankV1.Web.Extensions
{
    public static class HttpContextExtensions
    {
        public static User GetMySessionObject(this HttpContext current)
        {
            return (User)current?.Session["_SessionObject"];
        }

        public static void SetMySessionObject(this HttpContext current, User profile)
        {
            current.Session.Add("_SessionObject", profile);
        }
    }
}