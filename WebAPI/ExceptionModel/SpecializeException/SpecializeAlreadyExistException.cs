namespace WebAPI.ExceptionModel.SpecializeException
{
  public class SpecializeAlreadyExistException : System.Exception
  {
    public SpecializeAlreadyExistException() : base("Specialize already exist")
    {
    }

    public SpecializeAlreadyExistException(string msg) : base(msg)
    {
    }
  }
}