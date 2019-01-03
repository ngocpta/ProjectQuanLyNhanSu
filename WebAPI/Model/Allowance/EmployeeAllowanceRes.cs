namespace WebAPI.Model.Allowance
{
    public class EmployeeAllowanceRes
    {
        public string Id { get; set; }
        
        public string EmployeeId { get; set; }
        
        public string AllowanceId { get; set; }
        
        public string SigningDate { get; set; }
        
        public string Note { get; set; }
    }
}