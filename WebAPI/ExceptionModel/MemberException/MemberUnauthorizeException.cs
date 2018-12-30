namespace WebAPI.ExceptionModel.MemberException
{
  public class MemberUnauthorizeException : System.Exception
  {
    public MemberUnauthorizeException() : base("member is unauthorized")
    {
    }

    public MemberUnauthorizeException(string msg) : base(msg)
    {
    }
  }
}