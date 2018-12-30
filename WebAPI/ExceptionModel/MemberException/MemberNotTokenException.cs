namespace WebAPI.ExceptionModel.MemberException
{
  public class MemberNotTokenException : System.Exception
  {
    public MemberNotTokenException() : base("member have not token")
    {
    }

    public MemberNotTokenException(string msg) : base(msg)
    {
    }
  }
}