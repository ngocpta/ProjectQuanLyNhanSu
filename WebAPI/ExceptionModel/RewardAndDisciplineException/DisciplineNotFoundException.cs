namespace WebAPI.ExceptionModel.RewardAndDisciplineException
{
  public class DisciplineNotFoundException : System.Exception
  {
    public DisciplineNotFoundException() : base("discipline not found")
    {
    }

    public DisciplineNotFoundException(string msg) : base(msg)
    {
    }
  }
}