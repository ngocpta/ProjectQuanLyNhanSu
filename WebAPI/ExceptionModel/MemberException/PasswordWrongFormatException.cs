namespace WebAPI.ExceptionModel.MemberException
{
  public class PasswordWrongFormatException : System.Exception
  {
    public PasswordWrongFormatException() : base("password is wrong format")
    {
    }

    public PasswordWrongFormatException(string msg) : base(msg)
    {
    }
  }
}