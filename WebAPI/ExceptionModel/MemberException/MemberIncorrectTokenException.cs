namespace WebAPI.ExceptionModel.MemberException
{
  public class MemberIncorrectTokenException : System.Exception
  {
    public MemberIncorrectTokenException() : base("member is disabled or deleted")
    {
    }

    public MemberIncorrectTokenException(string msg) : base(msg)
    {
    }
  }
}