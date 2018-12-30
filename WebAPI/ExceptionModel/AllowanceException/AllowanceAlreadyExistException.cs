namespace WebAPI.ExceptionModel.AllowanceException
{
  public class AllowanceAlreadyExistException : System.Exception
  {
    public AllowanceAlreadyExistException() : base("Allowance already exist")
    {
    }

    public AllowanceAlreadyExistException(string msg) : base(msg)
    {
    }
  }
}