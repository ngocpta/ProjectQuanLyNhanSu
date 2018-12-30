namespace WebAPI.ExceptionModel.ContractException
{
  public class ContractAlreadyExistException : System.Exception
  {
    public ContractAlreadyExistException() : base("contract already exist")
    {
    }

    public ContractAlreadyExistException(string msg) : base(msg)
    {
    }
  }
}