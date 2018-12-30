namespace WebAPI.ExceptionModel.MemberException
{
  public class MemberNotLoginException : System.Exception
  {
    public MemberNotLoginException() : base("member is not login")
    {
    }

    public MemberNotLoginException(string msg) : base(msg)
    {
    }
  }
}