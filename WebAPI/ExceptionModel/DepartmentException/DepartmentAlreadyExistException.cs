namespace WebAPI.ExceptionModel.DepartmentException
{
  public class DepartmentAlreadyExistException : System.Exception
  {
    public DepartmentAlreadyExistException() : base("department already exist")
    {
    }

    public DepartmentAlreadyExistException(string msg) : base(msg)
    {
    }
  }
}