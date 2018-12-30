namespace WebAPI.ExceptionModel
{
  public class DataInvalidException : System.Exception
  {
    public DataInvalidException() : base("Data have changed")
    {
    }

    public DataInvalidException(string msg) : base(msg)
    {
    }
  }
}