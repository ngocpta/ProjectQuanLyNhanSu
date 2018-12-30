namespace WebAPI.Model.Contract
{
    public class ContractModelReq
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string ContractTypeId { get; set; }
        public int? Time { get; set; }
        public bool? Active { get; set; }
    }
}