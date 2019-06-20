namespace XboxStatistics.Models
{
    public class Stat
    {
        public long Xuid { get; set; }
        public string Scid { get; set; }
        public int? Titleid { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}