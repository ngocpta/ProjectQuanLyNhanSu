using Newtonsoft.Json;
using WebAPI.Security;

namespace WebAPI.Model
{
  public class Req<T> where T: class 
  {
        public T value { get; set; }
        public string hashed { get; set; }

        public string Json() => JsonConvert.SerializeObject(value);

        public bool IsValid(string secretKey)
        {
            var jsonReq = Json() + secretKey;
            var jsonHashed = HashPass.HashSHA256(jsonReq);
            return hashed.Equals(jsonHashed);
        }
    }
}