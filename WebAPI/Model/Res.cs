namespace WebAPI.Model
{
  public class Res
  {
        public const string Success = "SUCCESS";
        public const string Error = "ERROR";

        public string Status { get; set; }
        public object Value { get; set; }

        public Res()
        {
        }

        public Res(object value) : this(Success, value)
        {
        }

        public Res(string status, object value = null)
        {
            Status = status;
            Value = value;
        }
    }
}