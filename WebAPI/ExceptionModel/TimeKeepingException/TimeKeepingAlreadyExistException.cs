namespace WebAPI.ExceptionModel.TimeKeepingException
{
    public class TimeKeepingAlreadyExistException:System.Exception
    {
        public TimeKeepingAlreadyExistException() : base("time keeping already exist")
        {
        }

        public TimeKeepingAlreadyExistException(string msg) : base(msg)
        {
        }
    }
}