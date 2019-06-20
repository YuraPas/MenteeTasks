namespace XboxStatistics.Models
{
    public class StatGroup
    {
        public string Name { get; set; }
        public int TitleId { get; set; }
        public StatlistsCollection[] StatlistsCollection { get; set; }
    }
}