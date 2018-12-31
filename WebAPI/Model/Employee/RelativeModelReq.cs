namespace WebAPI.Model.Employee
{
    public class RelativeModelReq
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string Fullname { get; set; }
        public string Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Career { get; set; }
        public int? WardId { get; set; }
        public int? Relationship { get; set; }
    }
}