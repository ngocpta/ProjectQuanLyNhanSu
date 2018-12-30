namespace WebAPI.ExceptionModel.MemberException
{
  public class MemberNotFoundException : System.Exception
  {
    public MemberNotFoundException() : base("member not found")
    {
    }

    public MemberNotFoundException(string msg) : base(msg)
    {
    }
  }
}