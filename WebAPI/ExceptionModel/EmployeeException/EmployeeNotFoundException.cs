using System;

namespace WebAPI.ExceptionModel.EmployeeException
{
    public class EmployeeNotFoundException:Exception
    {
        public EmployeeNotFoundException() : base("Employee not found")
        {
        }

        public EmployeeNotFoundException(string msg) : base(msg)
        {
        }
    }
}