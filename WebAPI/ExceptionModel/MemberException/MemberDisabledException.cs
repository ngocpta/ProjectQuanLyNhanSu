namespace WebAPI.ExceptionModel.MemberException
{
  public class MemberDisabledException : System.Exception
  {
    public MemberDisabledException() : base("member is disabled or deleted")
    {
    }

    public MemberDisabledException(string msg) : base(msg)
    {
    }
  }
}