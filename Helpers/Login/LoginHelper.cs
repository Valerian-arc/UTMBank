using System;
using System.Security.Cryptography;
using System.Text;

namespace Helpers.Login
{
    public static class LoginHelper
    {
        public static string HashGen(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var orinialBytes = Encoding.Default.GetBytes(password + "hd7gGDa7HG12");
            var encodedBytes = md5.ComputeHash(orinialBytes);

            return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
        }
    }
}
