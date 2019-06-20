namespace XboxStatistics.Models
{
    public class Requirement
    {
        public string Id { get; set; }
        public double? Current { get; set; }
        public double Target { get; set; }
        public string OperationType { get; set; }
        public string ValueType { get; set; }
        public string RuleParticipationType { get; set; }
    }
}