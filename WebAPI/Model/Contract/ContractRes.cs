namespace WebAPI.Model.Contract
{
    public class ContractRes
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string ContractTypeId { get; set; }
        public string ContractTypeName { get; set; }
        public int? Time { get; set; }
        public bool? Active { get; set; }
        public decimal? SalaryFactor { get; set; }
    }
}