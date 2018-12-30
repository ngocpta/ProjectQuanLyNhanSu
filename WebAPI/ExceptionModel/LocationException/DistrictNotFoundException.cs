namespace WebAPI.ExceptionModel.MemberException
{
  public class DistrictNotFoundException : System.Exception
  {
    public DistrictNotFoundException() : base("District not found")
    {
    }

    public DistrictNotFoundException(string msg) : base(msg)
    {
    }
  }
}