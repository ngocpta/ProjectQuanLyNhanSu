namespace WebAPI.ExceptionModel.MemberException
{
  public class PasswordNotMatchException : System.Exception
  {
    public PasswordNotMatchException() : base("password not match")
    {
    }

    public PasswordNotMatchException(string msg) : base(msg)
    {
    }
  }
}