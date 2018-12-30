namespace WebAPI.ExceptionModel.RewardAndDisciplineException
{
  public class RewardAlreadyExistException : System.Exception
  {
    public RewardAlreadyExistException() : base("reward already exist")
    {
    }

    public RewardAlreadyExistException(string msg) : base(msg)
    {
    }
  }
}