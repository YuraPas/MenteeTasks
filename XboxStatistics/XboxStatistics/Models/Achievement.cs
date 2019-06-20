namespace XboxStatistics.Models
{
    public class Achievement
    {
        public int Id { get; set; }
        public string ServiceConfigId { get; set; }
        public string Name { get; set; }
        public Titleassociation[] TitleAssociations { get; set; }
        public string ProgressState { get; set; }
        public Progression Progression { get; set; }
        public MediaAsset[] MediaAssets { get; set; }
        public string[] Platforms { get; set; }
        public bool IsSecret { get; set; }
        public string Description { get; set; }
        public string LockedDescription { get; set; }
        public string ProductId { get; set; }
        public string AchievementType { get; set; }
        public string ParticipationType { get; set; }
        public object TimeWindow { get; set; }
        public Reward[] Rewards { get; set; }
        public string EstimatedTime { get; set; }
        public object Deeplink { get; set; }
        public bool IsRevoked { get; set; }
        public Rarity Rarity { get; set; }
    }
}