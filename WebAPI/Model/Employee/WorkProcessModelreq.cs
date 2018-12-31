namespace WebAPI.Model.Employee
{
    public class WorkProcessModelreq
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string CompanyWorkedName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string StartWork { get; set; }
        public string EndWork { get; set; }
    }
}