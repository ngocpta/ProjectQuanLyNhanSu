namespace WebAPI.ExceptionModel.AllowanceException
{
  public class AllowanceNotFoundException : System.Exception
  {
    public AllowanceNotFoundException() : base("allowance not found")
    {
    }

    public AllowanceNotFoundException(string msg) : base(msg)
    {
    }
  }
}