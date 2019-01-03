namespace WebAPI.ExceptionModel.AllowanceException
{
  public class EmployeeDisciplineAlreadyExistException : System.Exception
  {
    public EmployeeDisciplineAlreadyExistException() : base("employee discipline already exist")
    {
    }

    public EmployeeDisciplineAlreadyExistException(string msg) : base(msg)
    {
    }
  }
}