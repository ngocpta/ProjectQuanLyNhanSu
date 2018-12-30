namespace WebAPI.ExceptionModel.MemberException
{
  public class WardNotFoundException : System.Exception
  {
    public WardNotFoundException() : base("Ward not found")
    {
    }

    public WardNotFoundException(string msg) : base(msg)
    {
    }
  }
}