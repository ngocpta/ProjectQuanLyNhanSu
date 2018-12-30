namespace WebAPI.ExceptionModel.ContractException
{
  public class ContractTypeNotFoundException : System.Exception
  {
    public ContractTypeNotFoundException() : base("contract type not found")
    {
    }

    public ContractTypeNotFoundException(string msg) : base(msg)
    {
    }
  }
}