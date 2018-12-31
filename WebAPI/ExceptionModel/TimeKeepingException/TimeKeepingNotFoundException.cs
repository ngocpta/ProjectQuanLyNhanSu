namespace WebAPI.ExceptionModel.TimeKeepingException
{
  public class TimeKeepingNotFoundException : System.Exception
  {
    public TimeKeepingNotFoundException() : base("time keeping not found")
    {
    }

    public TimeKeepingNotFoundException(string msg) : base(msg)
    {
    }
  }
}