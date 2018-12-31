namespace WebAPI.ExceptionModel.EmployeeException
{
    public class EmployeeAlreadyExistException: System.Exception
    {
        public EmployeeAlreadyExistException() : base("Employee already exist")
        {
        }

        public EmployeeAlreadyExistException(string msg) : base(msg)
        {
        }
    }
}