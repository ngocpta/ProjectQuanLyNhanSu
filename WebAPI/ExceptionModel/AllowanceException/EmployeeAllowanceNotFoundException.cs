namespace WebAPI.ExceptionModel.AllowanceException
{
  public class EmployeeAllowanceNotFoundException : System.Exception
  {
    public EmployeeAllowanceNotFoundException() : base("employee allowance not found")
    {
    }

    public EmployeeAllowanceNotFoundException(string msg) : base(msg)
    {
    }
  }
}