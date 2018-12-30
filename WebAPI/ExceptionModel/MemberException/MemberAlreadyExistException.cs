namespace WebAPI.ExceptionModel.MemberException
{
  public class MemberAlreadyExistException : System.Exception
  {
    public MemberAlreadyExistException() : base("member is already exist")
    {
    }

    public MemberAlreadyExistException(string msg) : base(msg)
    {
    }
  }
}