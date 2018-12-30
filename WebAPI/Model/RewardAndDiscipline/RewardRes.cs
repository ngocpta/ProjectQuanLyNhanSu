namespace WebAPI.Model.RewardAndDiscipline
{
    public class RewardRes
    {
        public string Id { get; set; }
        public string DicisionNo { get; set; }
        public string EffectiveDate { get; set; }
        public string Title { get; set; }
        public string RewardAndDisciplineMethodId { get; set; }
        public string RewardAndDisciplineMethodName { get; set; }
        public double? Amount { get; set; }
        public double? PercentReward { get; set; }
        public string SignDate { get; set; }
        public string SignBy { get; set; }
        public string Desciption { get; set; }
    }
}