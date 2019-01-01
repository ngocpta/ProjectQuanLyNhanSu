namespace WebAPI.ExceptionModel.SalaryException
{
  public class SalaryNotFoundException : System.Exception
  {
    public SalaryNotFoundException() : base("salary not found")
    {
    }

    public SalaryNotFoundException(string msg) : base(msg)
    {
    }
  }
}