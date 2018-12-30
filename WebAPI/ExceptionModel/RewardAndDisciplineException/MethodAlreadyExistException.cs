namespace WebAPI.ExceptionModel.RewardAndDisciplineException
{
  public class MethodAlreadyExistException : System.Exception
  {
    public MethodAlreadyExistException() : base("reward and discipline method already exist")
    {
    }

    public MethodAlreadyExistException(string msg) : base(msg)
    {
    }
  }
}