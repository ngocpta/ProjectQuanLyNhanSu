namespace WebAPI.ExceptionModel.DepartmentException
{
  public class DepartmentNotFoundException : System.Exception
  {
    public DepartmentNotFoundException() : base("department not found")
    {
    }

    public DepartmentNotFoundException(string msg) : base(msg)
    {
    }
  }
}