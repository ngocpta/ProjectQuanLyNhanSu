namespace WebAPI.ExceptionModel.RewardAndDisciplineException
{
  public class MethodNotFoundException : System.Exception
  {
    public MethodNotFoundException() : base("reward and discipline method not found")
    {
    }

    public MethodNotFoundException(string msg) : base(msg)
    {
    }
  }
}