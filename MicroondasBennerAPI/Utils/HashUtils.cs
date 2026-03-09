using System.Security.Cryptography;
using System.Text;

namespace MicroondasBennerAPI.Utils
{
    public static class HashUtils
    {
        public static string GerarSha1(string texto)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(texto);
            byte[] hash = SHA1.HashData(bytes);

            StringBuilder sb = new();

            foreach (byte b in hash)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }
}