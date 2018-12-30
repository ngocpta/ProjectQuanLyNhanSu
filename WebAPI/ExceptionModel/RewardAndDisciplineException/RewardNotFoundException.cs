namespace WebAPI.ExceptionModel.RewardAndDisciplineException
{
  public class RewardNotFoundException : System.Exception
  {
    public RewardNotFoundException() : base("reward not found")
    {
    }

    public RewardNotFoundException(string msg) : base(msg)
    {
    }
  }
}