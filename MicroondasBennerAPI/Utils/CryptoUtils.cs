using System.Security.Cryptography;
using System.Text;

namespace MicroondasBennerAPI.Utils
{
    public static class CryptoUtils
    {
        private static readonly string chave = Environment.GetEnvironmentVariable("CRYPTO_KEY") ?? string.Empty;

        public static string Encrypt(string texto)
        {
            using var aes = Aes.Create();

            var key = Encoding.UTF8.GetBytes(chave.PadRight(32));

            aes.Key = key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();

            ms.Write(aes.IV, 0, aes.IV.Length);

            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(texto);
            }

            var bytes = ms.ToArray();

            return Convert.ToHexString(bytes);
        }

        public static string Decrypt(string textoHex)
        {
            var fullCipher = Convert.FromHexString(textoHex);

            using var aes = Aes.Create();

            var key = Encoding.UTF8.GetBytes(chave.PadRight(32));

            var iv = new byte[16];

            Array.Copy(fullCipher, iv, iv.Length);

            aes.Key = key;
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream(fullCipher, 16, fullCipher.Length - 16);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}
