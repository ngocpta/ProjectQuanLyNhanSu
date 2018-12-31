namespace WebAPI.ExceptionModel.PostionException
{
  public class PositionNotFoundException : System.Exception
  {
    public PositionNotFoundException() : base("Position not found")
    {
    }

    public PositionNotFoundException(string msg) : base(msg)
    {
    }
  }
}