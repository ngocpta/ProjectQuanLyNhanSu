namespace WebAPI.ExceptionModel.AllowanceException
{
  public class EmployeeDisciplineNotFoundException : System.Exception
  {
    public EmployeeDisciplineNotFoundException() : base("employee discipline not found")
    {
    }

    public EmployeeDisciplineNotFoundException(string msg) : base(msg)
    {
    }
  }
}