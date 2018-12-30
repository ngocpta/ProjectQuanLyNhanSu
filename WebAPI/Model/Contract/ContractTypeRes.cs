namespace WebAPI.Model.Contract
{
    public class ContractTypeRes
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Time { get; set; }
        public int? Type { get; set; }
        public string Note { get; set; }
        public bool? Active { get; set; }
    }
}