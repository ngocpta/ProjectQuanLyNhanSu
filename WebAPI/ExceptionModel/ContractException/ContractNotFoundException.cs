namespace WebAPI.ExceptionModel.ContractException
{
  public class ContractNotFoundException : System.Exception
  {
    public ContractNotFoundException() : base("contract not found")
    {
    }

    public ContractNotFoundException(string msg) : base(msg)
    {
    }
  }
}