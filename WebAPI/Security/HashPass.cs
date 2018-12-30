using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using WebAPI.Model;


namespace WebAPI.Security
{
  public static class HashPass
  {
        private static string key = "luogdev@@";
        
        //mã hóa mã Hash
        public static string HashSHA256(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString+key.Trim());
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
        /*public static bool IsValid(this Req<RateReq> req, string secretKey)
        {
            var jsonReq = JsonConvert.SerializeObject(req.value) + secretKey;
            var jsonHashed = HashSHA256(jsonReq);
            return req.hashed.Equals(jsonHashed);
        }*/
        
    }
}