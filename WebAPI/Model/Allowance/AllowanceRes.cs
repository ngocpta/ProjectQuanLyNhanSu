namespace WebAPI.Model.Allowance
{
    public class AllowanceRes
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
        public string EffectiveDate { get; set; }
        public double? Factor { get; set; }
        public string Note { get; set; }
    }
}