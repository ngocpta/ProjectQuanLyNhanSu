namespace WebAPI.ExceptionModel.ContractException
{
    public class ContractTypeDisableException:System.Exception
    {
        public ContractTypeDisableException() : base("contract type disabled")
        {
        }

        public ContractTypeDisableException(string msg) : base(msg)
        {
        }
    }
}