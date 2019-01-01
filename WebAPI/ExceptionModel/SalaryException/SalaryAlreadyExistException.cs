namespace WebAPI.ExceptionModel.SalaryException
{
  public class SalaryAlreadyExistException : System.Exception
  {
    public SalaryAlreadyExistException() : base("salary already exist")
    {
    }

    public SalaryAlreadyExistException(string msg) : base(msg)
    {
    }
  }
}