namespace WebAPI.ExceptionModel.SpecializeException
{
  public class SpecializeNotFoundException : System.Exception
  {
    public SpecializeNotFoundException() : base("Specialize not found")
    {
    }

    public SpecializeNotFoundException(string msg) : base(msg)
    {
    }
  }
}