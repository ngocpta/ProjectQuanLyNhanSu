namespace WebAPI.ExceptionModel.PostionException
{
  public class PositionAlreadyExistException : System.Exception
  {
    public PositionAlreadyExistException() : base("Position already exist")
    {
    }

    public PositionAlreadyExistException(string msg) : base(msg)
    {
    }
  }
}