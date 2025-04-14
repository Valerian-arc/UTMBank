using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace Helpers.Login
{
    public class CookieGenerator
    {
        public static string Create(string value)
        {
            return CookieGeneratorHelper.EncryptStringAes(
                value,
                "BjXNmq5MKKaraLwxz9uaATvFwE4Rj679KguTRE8c2j56FnkuKJKfkGbZEeDGFDvsGYNHpUXFUUUuUHBR4UV3T2kumguhubg6Gpt7CyqGDbUPrMvPc67kX3yP");
        }

        public static string Validate(string value)
        {
            return CookieGeneratorHelper.DecryptStringAes(
                value, 
                "BjXNmq5MKKaraLwxz9uaATvFwE4Rj679KguTRE8c2j56FnkuKJKfkGbZEeDGFDvsGYNHpUXFUUUuUHBR4UV3T2kumguhubg6Gpt7CyqGDbUPrMvPc67kX3yP");
        }
    }
}
