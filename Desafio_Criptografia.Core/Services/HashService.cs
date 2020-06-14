using System;
using System.Text;

namespace Desafio_Criptografia.Core.Services
{
    public class HashService
    {
        public static string GetHashSha1(string str)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var byteArray = Encoding.UTF8.GetBytes(str);
            var hash = sha1.ComputeHash(byteArray);
            var sb = new StringBuilder();

            foreach (var hashByte in hash)
            {
                sb.AppendFormat("{0:x2}", hashByte);
            }

            return sb.ToString();
        }
    }
}
