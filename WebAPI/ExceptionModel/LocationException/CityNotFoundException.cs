namespace WebAPI.ExceptionModel.LocationException
{
  public class ProvinceNotFoundException : System.Exception
  {
    public ProvinceNotFoundException() : base("province not found")
    {
    }

    public ProvinceNotFoundException(string msg) : base(msg)
    {
    }
  }
}