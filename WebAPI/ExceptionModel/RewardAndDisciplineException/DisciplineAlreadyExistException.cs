namespace WebAPI.ExceptionModel.RewardAndDisciplineException
{
  public class DisciplinereadyExistException : System.Exception
  {
    public DisciplinereadyExistException() : base("discipline already exist")
    {
    }

    public DisciplinereadyExistException(string msg) : base(msg)
    {
    }
  }
}