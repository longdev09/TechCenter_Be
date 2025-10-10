using System.Security.Cryptography;
using System.Text;

namespace TechCenter.Helpers
{
    public static class HashHelper
    {
        public static string GetMd5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var builder = new StringBuilder();

                foreach (var b in data)
                {
                    builder.Append(b.ToString("x2")); // x2 = lowercase hex
                }
                return builder.ToString();
            }
        }
    }
}
