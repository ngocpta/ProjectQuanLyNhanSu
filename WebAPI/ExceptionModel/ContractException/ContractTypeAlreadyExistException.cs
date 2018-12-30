namespace WebAPI.ExceptionModel.ContractException
{
  public class ContractTypeAlreadyExistException : System.Exception
  {
    public ContractTypeAlreadyExistException() : base("contract type already exist")
    {
    }

    public ContractTypeAlreadyExistException(string msg) : base(msg)
    {
    }
  }
}