namespace WebAPI.ExceptionModel.AllowanceException
{
  public class EmployeeAllowanceAlreadyExistException : System.Exception
  {
    public EmployeeAllowanceAlreadyExistException() : base("employee allowance already exist")
    {
    }

    public EmployeeAllowanceAlreadyExistException(string msg) : base(msg)
    {
    }
  }
}